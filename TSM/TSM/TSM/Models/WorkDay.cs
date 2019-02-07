using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Models
{
    public class WorkDay
    {
        public DayOfWeek Day { get; set; }

        public List<WorkHour> Hours { get; set; }
    }
}
