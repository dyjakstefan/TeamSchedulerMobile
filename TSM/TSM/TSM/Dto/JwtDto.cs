using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Dto
{
    public class JwtDto
    {
        public string Token { get; set; }

        public long Expires { get; set; }
    }
}
