namespace BeMo.Services
{
    public class RefreshStravaTokensRepeatingService : BackgroundService
    {
        private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(5));
        private readonly ILogger<RefreshStravaTokensRepeatingService> _logger;


        public RefreshStravaTokensRepeatingService(ILogger<RefreshStravaTokensRepeatingService> logger)
        {
            _logger = logger;
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
            Console.WriteLine("Executing background service");
        }
    }
}
