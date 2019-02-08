using System;
using System.Collections.Generic;
using TSM.Enums;

namespace TSM.Models
{
    public class Member : Entity
    {
        public int Hours { get; set; }

        public JobTitle Title { get; set; }

        public bool IsPartTime { get; set; }

        public int UserId { get; set; }

        public int TeamId { get; set; }

        public User User { get; set; }

        public TimeSpan AssignedTime { get; set; }

        public List<WorkUnit> WorkUnits { get; set; }

        public string DisplayAssignedTime => $"{AssignedTime:hh\\:mm}";

        public string DisplayHours => $"{Hours}:00";

        public bool IsOvertime => AssignedTime.Hours > Hours;
    }
}
