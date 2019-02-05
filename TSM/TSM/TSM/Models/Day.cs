using System;
using System.Collections.Generic;

namespace TSM.Models
{
    public class Day : Entity
    {
        public bool IsAccepted { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public Schedule Schedule { get; set; }

        public List<WorkUnit> UnitsOfWorks { get; set; }
    }
}
