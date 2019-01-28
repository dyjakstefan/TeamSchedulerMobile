using System;
using System.Collections.Generic;
using System.Text;
using TSM.Enums;

namespace TSM.Dto
{
    public class MemberDto
    {
        public int MemberId { get; set; }

        public int TeamId { get; set; }

        public string Email { get; set; }

        public int Hours { get; set; }

        public JobTitle Title { get; set; }

        public bool IsPartTime { get; set; }
    }
}
