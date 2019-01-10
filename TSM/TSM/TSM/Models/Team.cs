using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Models
{
    public class Team : Entity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public virtual List<Member> Members { get; set; }

        public virtual List<Schedule> Schedules { get; set; }
    }
}
