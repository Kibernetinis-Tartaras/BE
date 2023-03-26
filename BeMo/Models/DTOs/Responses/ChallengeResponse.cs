namespace BeMo.Models.DTOs.Responses
{
    public class ChallengeResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ActivityType Type { get; set; }

        public bool IsPublic { get; set; } = false;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public static implicit operator ChallengeResponse?(Challenge? challenge)
        {
            if (challenge == null) return null;

            return new ChallengeResponse
            {
                Id = challenge.Id,
                Name = challenge.Name,
                Type = challenge.Type,
                IsPublic = challenge.IsPublic,
                StartDate = challenge.StartDate,
                EndDate = challenge.EndDate,
            };
        }
    }
}
