using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Models
{
    public class JwtToken : Entity
    {
        public string Token { get; set; }

        public long Expires { get; set; }

        public string Email { get; set; }
    }
}
