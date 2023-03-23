using System.Text.Json.Serialization;

namespace BeMo.Models
{
    public class ChallengeUser
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }

        public User User { get; set; }

        [JsonIgnore]
        public long ChallengeId { get; set; }

        public Challenge Challenge { get; set; } = null!;
    }
}
