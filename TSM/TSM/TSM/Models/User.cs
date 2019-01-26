using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TSM.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public string Email { get; set; }

        public List<Member> Members { get; set; }
    }
}
