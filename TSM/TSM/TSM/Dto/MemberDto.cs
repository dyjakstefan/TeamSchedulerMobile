using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Dto
{
    public class MemberDto
    {
        public int TeamId { get; set; }

        public string Email { get; set; }

        public int Hours { get; set; }

        public bool IsPartTime { get; set; }
    }
}
