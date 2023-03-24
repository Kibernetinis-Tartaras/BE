namespace BeMo.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public long StravaUserId { get; set; }

        public string Username { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public string AccessToken { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime LastActivitySync { get; set; } = DateTime.UtcNow;
    }
}
