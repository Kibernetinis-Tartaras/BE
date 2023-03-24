namespace BeMo.Constants
{
    public static class StravaConstants
    {
        internal const string BaseUrl = "https://www.strava.com/";
        internal const string RefreshTokenEndpoint = "oauth/token";
        internal const string ListAthleteActivitiesEndpoint = "api/v3/athlete/activities";
        internal static TimeSpan RefreshTokenBackgroundServiceTimer = new TimeSpan(5,0,0);
        internal static TimeSpan DataRetrievalBackgroundServiceTimer = new TimeSpan(0,0,30);
        internal static int ActivitiesPerRequest = 100;
    }
}
