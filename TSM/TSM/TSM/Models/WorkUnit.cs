using System;

namespace TSM.Models
{
    public class WorkUnit : Entity
    {
        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public string Description { get; set; }

        public int ScheduleId { get; set; }

        public int MemberId { get; set; }

        public Member Member { get; set; }

        public string DisplayWorkUnit => $"{Start:hh\\:mm} - {End:hh\\:mm}";
    }
}
