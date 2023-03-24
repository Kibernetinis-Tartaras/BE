namespace BeMo.Models.DTOs.Responses
{
    public class StravaGetAthleteActivitiesResponse
    {
        public List<StravaActivityResponse> activities { get; set; } = null!;
    }

    public class StravaActivityResponse
    {
        public long elapsed_time { get; set; }
        public string type { get; set; }
        public double distance { get; set; }
        public double max_speed { get; set; }
        public double average_speed { get; set; }
        public DateTime start_date { get; set; }
        public double total_elevation_gain { get; set; }
    }
}
