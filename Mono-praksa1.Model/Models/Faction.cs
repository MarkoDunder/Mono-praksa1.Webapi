using Mono_praksa1.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono_praksa1.Model.Models
{
    public class Faction:IFaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Members { get; set; }

        public Faction(int id, string name, int members)
        {
            Id = id;
            Name = name;
            Members = members;
        }

        public bool IsStrong()
        {
            if (Members < 2000)
                return false;
            else
                return true;
        }
    }
}
