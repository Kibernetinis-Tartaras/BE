namespace BeMo.Models
{
    public class User_Challenge
    {
        public int Id { get; set; }

        public int ChallengeId { get; set; }

        public Challenge Challenge { get; set; } = null!;

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
