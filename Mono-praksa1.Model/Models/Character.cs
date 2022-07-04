using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono_praksa1.Model.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public int Id { get; set; }

        public Character() { }
        public Character(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;

        }
    }
}
