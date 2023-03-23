using System.Text.Json.Serialization;

namespace BeMo.Models
{
    // to be updated when authentiacation is figured out
    public class StravaCredentials
    {
        [JsonIgnore]
        public long Id { get; set; }

        public long RefreshToken { get; set; }

        private string AuthenticationToken { get; set; }
    }
}
