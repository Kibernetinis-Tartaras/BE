﻿namespace BeMo.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public double Distance { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan Elapsed { get; set; }

        public double ElevationGain { get; set; }

        public double MaxSpeed { get; set; }

        public double AverageSpeed { get; set; }

        public ActivityType ActivityType { get; set; }
    }
}
