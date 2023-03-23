using System.Text.Json.Serialization;

namespace BeMo.Models
{
    public class Activity
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int Distance { get; set; }

        public ActivityType Type { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int Elapsed { get; set; }
    }
}
