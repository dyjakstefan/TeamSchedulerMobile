using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Models
{
    public class Schedule : Entity
    {
        public bool IsAccepted { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public virtual List<Day> Days { get; set; }
    }
}
