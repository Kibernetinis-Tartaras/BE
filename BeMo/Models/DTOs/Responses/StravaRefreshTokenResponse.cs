namespace BeMo.Models.DTOs.Responses
{
    public class StravaRefreshTokenResponse
    {
        public string access_token { get; set; } = null!;
        public string refresh_token { get; set; } = null!;
    }
}
