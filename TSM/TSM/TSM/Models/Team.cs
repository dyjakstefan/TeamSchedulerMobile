using System.Collections.Generic;

namespace TSM.Models
{
    public class Team : Entity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public List<Member> Members { get; set; }

        public List<Schedule> Schedules { get; set; }
    }
}
