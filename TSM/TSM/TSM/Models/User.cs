using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TSM.Models
{
    [Table("users")]
    public class User : Entity
    {
        [MaxLength(250)]
        public string FirstName { get; set; }

        [MaxLength(250)]
        public string LastName { get; set; }

        [MaxLength(250), Unique]
        public string Email { get; set; }

        [MaxLength(25)]
        public string PhoneNumber { get; set; }

        [Ignore]
        public List<Member> Members { get; set; }
    }
}
