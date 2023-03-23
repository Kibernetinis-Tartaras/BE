namespace BeMo.Models
{
    public class User
    {

        public long Id { get; set; }

        public string? Name { get; set; }

        public string StravaId { get; set; }
        
        public string? Email { get; set; }

        public string? Surname { get; set; }

        public string? Phone { get; set; }

        public int StravaCredentialsId { get; set; }

        public StravaCredentials? StravaCredentials { get; set; }

        public List<Activity> Activities = new();
    }
}
