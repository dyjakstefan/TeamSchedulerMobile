using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Models
{
    public class Jwt : Entity
    {
        public string Token { get; set; }

        public long Expires { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
