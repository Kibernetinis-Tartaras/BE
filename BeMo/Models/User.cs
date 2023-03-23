namespace BeMo.Models
{
    public class User
    {

        public Guid Id { get; set; }

        public string? Name { get; set; }
        
        public string? Email { get; set; }

        public string? Surname { get; set; }

        public string? Phone { get; set; }

        public StravaCredentials? StravaCredentials { get; set; }

        public int Weight { get; set; }

        public List<Activity> Activities = new();



    }
}
