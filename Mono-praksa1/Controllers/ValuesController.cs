using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mono_praksa1.Controllers
{
    public class ValuesController : ApiController
    {
        public static class CharacterList
        {
            static List<Character> characters;
            static CharacterList()
            {
                characters = new List<Character>();
            }
            public static void Insert(Character character)
            {
                characters.Add(character);
            }

            public static List<Character> Show()
            {
                return characters;
            }

            public static void FindAndUpdate(int id, Character character)
            {
                foreach(var ch in characters)
                {
                    if (ch.Id == id)
                    {
                        ch.Name = character.Name;
                        ch.Surname = character.Surname;
                        ch.Id = character.Id;
                    }
                }
            }

            public static void FindAndDelete(int id)
            {
                for(int i = 0; i < characters.Count; i++)
                {
                    if (characters[i].Id == id)
                        characters.RemoveAt(i);
                }
                
            }
        }
            // GET api/values
            public IEnumerable<string> Get()
        {
            return new string[] { "Marko", "Dunder" };
        }

        public class Character {
            public string Name { get; set; }
            public string Surname { get; set; }

            public int Id { get; set; }

            public Character() { }
            public Character(string name, string surname, int id)
            {
                Name = name;
                Surname = surname;
                Id = id;
            }

        }



        // GET api/values/7
        public string Get(int id)
        {
            Character Jozo = new Character("Jozo", "Josipović", 12);
            return Jozo.Surname;
        }

        // POST api/values
        public List<Character> Post()
        {
            Character npc1 = new Character("Gaunther", "oDimm", 1);
            Character npc2 = new Character("Detlaff", "Regis", 2);
            Character npc3 = new Character("Sigismund", "Dijstra", 3);
            Character main = new Character("Geralt", "ofRivia", 4);
            CharacterList.Insert(npc1);
            CharacterList.Insert(npc2);
            CharacterList.Insert(npc3);
            CharacterList.Insert(main);
            return CharacterList.Show();
        }

        // PUT api/values/3
        [HttpPut]
        public List<Character> UpdateValue(int id, [FromBody] Character character)
        {
            CharacterList.FindAndUpdate(id, character);
            return CharacterList.Show();
        }

        // DELETE api/values/5
        [HttpDelete]
        public List<Character> DeleteCharacter(int id)
        {
            CharacterList.FindAndDelete(id);
            return CharacterList.Show();

        }

        
        
    }
}
