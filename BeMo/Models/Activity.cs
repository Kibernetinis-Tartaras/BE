namespace BeMo.Models
{
    public class Activity
    {
    public int Id { get; set; }

    public int Distance { get; set; }

    public ActivityType Type { get; set; }

    public DateTime Start { get; set; }

    // This one we might calculate from start and end, since it is not included in the response of the StravaAPI
    public DateTime End { get; set; }

    public int Elapsed { get; set; }
    }
}
