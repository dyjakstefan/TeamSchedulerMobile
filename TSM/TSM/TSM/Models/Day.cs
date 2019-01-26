using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Models
{
    public class Day : Entity
    {
        public bool IsAccepted { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public Schedule Schedule { get; set; }

        public List<Task> UnitsOfWorks { get; set; }
    }
}
