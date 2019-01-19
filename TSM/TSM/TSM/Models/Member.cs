using System;
using System.Collections.Generic;
using System.Text;
using TSM.Enums;

namespace TSM.Models
{
    public class Member : Entity
    {
        public int Hours { get; set; }

        public Title Title { get; set; }

        public bool IsPartTime { get; set; }

        public int UserId { get; set; }

        public int TeamId { get; set; }

        public User User { get; set; }

        public Team Team { get; set; }

        public List<UnitOfWork> UnitsOfWork { get; set; }
    }
}
