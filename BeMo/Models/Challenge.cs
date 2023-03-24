namespace BeMo.Models
{
    public class Challenge
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ActivityType Type { get; set; }

        public bool IsPublic { get; set; } = false;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
