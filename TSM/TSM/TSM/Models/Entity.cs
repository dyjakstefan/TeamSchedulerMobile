using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TSM.Models
{
    public abstract class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
