namespace BeMo.Models.DTOs.Requests
{
    public class ChallengePostRequest
    {
            public int id { get; set; }

            public string name { get; set; }

            public int type { get; set; }

            public bool isPublic { get; set; }

            public DateTime startDate { get; set; }

            public DateTime endDate { get; set; }

    }
}
