using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono_praksa1.Model.Models
{
    public class FilteredCharacter
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public FilteredCharacter(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public FilteredCharacter() { }
    }
}
