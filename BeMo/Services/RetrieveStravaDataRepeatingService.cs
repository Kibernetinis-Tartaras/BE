﻿using BeMo.Constants;
using BeMo.Models;
using BeMo.Models.DTOs.Responses;
using BeMo.Options;
using BeMo.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BeMo.Services
{
    public class RetrieveStravaDataRepeatingService : BackgroundService
    {
        private readonly PeriodicTimer _timer = new(StravaConstants.DataRetrievalBackgroundServiceTimer);
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<RefreshStravaTokensRepeatingService> _logger;

        private readonly StravaOptions _stravaOptions;

        public RetrieveStravaDataRepeatingService(
            ILogger<RefreshStravaTokensRepeatingService> logger,
            IServiceProvider serviceProvider,
            IOptions<StravaOptions> stravaOptions)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _stravaOptions = stravaOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting background service for retrieving strava tokens");

            try
            {
                while (await _timer.WaitForNextTickAsync(cancellationToken))
                {
                    await fetchAndSaveStravaActivityData();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Background service for retrieving strava tokens was cancelled.");
            }

        }

        public async Task fetchAndSaveStravaActivityData()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();

                var _activityRepository = scope.ServiceProvider.GetRequiredService<IRepository<Activity>>();

                var users = await _userRepository.GetAllAsync();

                var dataURL = StravaConstants.BaseUrl + StravaConstants.ListAthleteActivitiesEndpoint;

                foreach (User user in users)
                {
                    if (user.AccessToken is not null)
                    {
                        try
                        {
                            int activitiesRetrieved;

                            var pageNumber = 0;
                            var before = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            var after = new DateTimeOffset(user.LastActivitySync).ToUnixTimeMilliseconds();
                            var per_page = StravaConstants.ActivitiesPerRequest;
                            do
                            {
                                pageNumber++;
                                string parameteres = $"?before={before}&after={after}&page={pageNumber}&per_page={per_page}";

                                var result = await _httpClient.GetAsync(String.Concat(dataURL, parameteres));

                                if (!result.IsSuccessStatusCode)
                                {
                                    throw new BadHttpRequestException("Couldn't retrieve activities");
                                }

                                var responseBody = await result.Content.ReadAsStringAsync();

                                var response = JsonConvert.DeserializeObject<StravaGetAthleteActivitiesResponse>(responseBody) ?? throw new JsonSerializationException("Wrong response object type retrieved");

                                var activities = response.activities;

                                activitiesRetrieved = activities.Count;

                                foreach (StravaActivityResponse activityResponse in activities)
                                {
                                    var activity = new Activity();

                                    switch (activityResponse.type)
                                    {
                                        case "Walk":
                                            activity.ActivityType = ActivityType.Walk;
                                            break;
                                        case "Run":
                                            activity.ActivityType = ActivityType.Run;
                                            break;
                                        case "Ride":
                                            activity.ActivityType = ActivityType.Cycle;
                                            break;
                                        default:
                                            continue;
                                    }

                                    activity.AverageSpeed = activityResponse.average_speed;
                                    activity.Elapsed = new TimeSpan(0, 0, (int)activityResponse.elapsed_time);
                                    activity.Distance = activityResponse.distance;
                                    activity.ElevationGain = activityResponse.total_elevation_gain;
                                    activity.MaxSpeed = activityResponse.max_speed;
                                    activity.StartDate = activityResponse.start_date;
                                    activity.EndDate = activityResponse.start_date.AddSeconds(activityResponse.elapsed_time);

                                    await _activityRepository.InsertAsync(activity);
                                }
                            }
                            while (activitiesRetrieved == StravaConstants.ActivitiesPerRequest);

                            user.LastActivitySync = DateTime.UtcNow;

                            await _userRepository.InsertAsync(user);
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e.Message);
                        }
                    }
                }
            }
        }
    }
}
