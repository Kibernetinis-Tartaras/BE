using System.Text.Json.Serialization;

namespace BeMo.Models
{
    public class Challenge
    {
        [JsonIgnore]
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public ActivityType Type { get; set; }

        public List<Admin> Admins = new();
        
        public List<ChallengeUser> Users = new();

    }
}
