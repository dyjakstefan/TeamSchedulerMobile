using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Models
{
    public class Day : Entity
    {
        public bool IsAccepted { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public virtual Schedule Schedule { get; set; }

        public virtual List<UnitOfWork> UnitsOfWorks { get; set; }
    }
}
