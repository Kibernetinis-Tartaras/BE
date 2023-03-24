using BeMo.Constants;
using BeMo.Models;
using BeMo.Models.DTOs.Responses;
using BeMo.Options;
using BeMo.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BeMo.Services
{
    public class RefreshStravaTokensRepeatingService : BackgroundService
    {
        private readonly PeriodicTimer _timer = new(StravaConstants.RefreshTokenBackgroundServiceTimer);
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<RefreshStravaTokensRepeatingService> _logger;
  
        private readonly StravaOptions _stravaOptions;

        public RefreshStravaTokensRepeatingService(
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
                    await refreshUserAccessTokens();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Background service for retrieving strava tokens was cancelled.");
            } 
            
        }

        public async Task refreshUserAccessTokens()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();

                var users = await _userRepository.GetAllAsync();

                string refreshURL = StravaConstants.BaseUrl + StravaConstants.RefreshTokenEndpoint;

                foreach (User user in users)
                {
                    if (user.RefreshToken is not null)
                    {
                        try
                        {
                            var grant_type = "refresh_token";
                            var refresh_token = user.RefreshToken;
                            var client_secret = _stravaOptions.ClientSecret;
                            var client_id = _stravaOptions.ClientId;

                            string parameteres = $"?grant_type={grant_type}&refresh_token={refresh_token}&client_secret={client_secret}&client_id={client_id}";

                            var response = await _httpClient.PostAsync(String.Concat(refreshURL, parameteres), null);

                            if (!response.IsSuccessStatusCode)
                            {
                                throw new BadHttpRequestException("Couldn't refresh token");
                            }

                            var responseString = await response.Content.ReadAsStringAsync();

                            var responseObject = JsonConvert.DeserializeObject<StravaRefreshTokenResponse>(responseString) ?? throw new JsonSerializationException("Wrong response object type retrieved");

                            user.AccessToken = responseObject.access_token;
                            user.RefreshToken = responseObject.refresh_token;

                            await _userRepository.UpdateAsync(user);
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
