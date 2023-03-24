namespace BeMo.Constants
{
    public static class StravaConstants
    {
        internal const string BaseUrl = "https://www.strava.com/";
        internal const string RefreshTokenEndpoint = "oauth/token";
        internal static TimeSpan RefreshTokenBackgroundServiceTimer = new TimeSpan(5,0,0);
    }
}
