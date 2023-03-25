namespace BeMo.Models.DTOs.Responses
{
    public class UserChallengesResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ActivityType Type { get; set; }

        public bool IsPublic { get; set; } = false;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public static implicit operator UserChallengesResponse?(Challenge? v)
        {
            throw new NotImplementedException();
        }
    }
}
