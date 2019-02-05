using System;

namespace TSM.Models
{
    public class Schedule : Entity
    {
        public bool IsAccepted { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public int TeamId { get; set; }

        public bool IsDescriptionNotEmpty
        {
            get { return !string.IsNullOrWhiteSpace(Description); }
        }
    }
}
