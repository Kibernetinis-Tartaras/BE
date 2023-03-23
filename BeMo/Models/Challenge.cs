namespace BeMo.Models
{
    public class Challenge
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public ActivityType Type { get; set; }

        public List<Admin> Admins = new();
        
        public List<ChallengeUser> Users = new();

    }
}
