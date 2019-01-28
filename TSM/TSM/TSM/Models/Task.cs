using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Models
{
    public class Task : Entity
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public int ScheduleId { get; set; }

        public int MemberId { get; set; }

        public Member Member { get; set; }
    }
}
