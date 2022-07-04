using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Mono_praksa1.Model.Models;

namespace Mono_praksa1.Controllers
{
    public class ValuesController : ApiController
    {
        public static string cs = @"Data Source=DESKTOP-RN0M5JB;Initial Catalog=master;Integrated Security=True";
        
        
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

        //public class Character {
        //    public string Name { get; set; }
        //    public string Surname { get; set; }

        //    public int Id { get; set; }

        //    public Character() { }
        //    public Character(int id,string name, string surname )
        //    {
        //        Id = id;
        //        Name = name;
        //        Surname = surname;
                
        //    }

        //}



        // GET api/values/7
        public HttpResponseMessage Get(int id)
        {
            SqlConnection connection = new SqlConnection(cs);
            string selector = "Select Id From Characters Where Id=@Id;";
            SqlCommand searchCommand = new SqlCommand(selector, connection);
            searchCommand.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = searchCommand.ExecuteReader();
            if (reader.HasRows)
            {
                Character character = new Character();
                character.Id = reader.GetInt32(0);
                character.Name = reader.GetString(1);
                character.Surname = reader.GetString(2);
                return Request.CreateResponse(HttpStatusCode.OK, character);
            }
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.NotFound, "did not find character with that ID");
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody] Character character)
        {
            SqlConnection connection = new SqlConnection(cs);
            
            string insertCharacter = "Insert into Characters(Id, Name, Surname) Values(@Id, @Name, @Surname); ";
            SqlCommand command = new SqlCommand(insertCharacter, connection);
            command.Parameters.AddWithValue("@Id", character.Id);
            command.Parameters.AddWithValue("@Name", character.Name);
            command.Parameters.AddWithValue("@Surname", character.Surname);

            string selector = "Select Id From Characters Where Id=@Id;";
            SqlCommand searchCommand = new SqlCommand(selector, connection);
            searchCommand.Parameters.AddWithValue("@Id", character.Id);

            connection.Open();
            SqlDataReader reader = searchCommand.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Id already exists");
                        
            }
            else
            {
                reader.Close();
                //SqlDataReader reader1 = command.ExecuteReader();
                command.ExecuteNonQuery();
                return Request.CreateResponse(HttpStatusCode.OK, "Insert valid");
            }


        }

        // PUT api/values/3
        [HttpPut]
        public HttpResponseMessage UpdateValue(int id, [FromBody] Character character)
        {
            //CharacterList.FindAndUpdate(id, character);
            //return CharacterList.Show();
            SqlConnection connection = new SqlConnection(cs);
            string updateCharacter = "Update Characters Set Id=@id, Name=@Name, Surname=@Surname Where Characters.Id=@Id; ";
            SqlCommand updatecmd = new SqlCommand(updateCharacter, connection);
            updatecmd.Parameters.AddWithValue("@Id", id);
            updatecmd.Parameters.AddWithValue("@Name", character.Name);
            updatecmd.Parameters.AddWithValue("@Surname", character.Surname);
            connection.Open(); 
            SqlDataReader reader = updatecmd.ExecuteReader();
            if (reader.HasRows)
            {
                
                    reader.Close();
                    updatecmd.ExecuteNonQuery();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "update completed");
                
            }
            
            return Request.CreateResponse(HttpStatusCode.BadRequest, "there is no character with that Id");


        }

        // DELETE api/values/5
        [HttpDelete]
        public HttpResponseMessage DeleteCharacter(int id)
        {
            //CharacterList.FindAndDelete(id);
            //return CharacterList.Show();

            SqlConnection connection = new SqlConnection(cs);
            string delete = "Delete from Characters where Characters.Id=@Id";
            SqlCommand deleteCommand = new SqlCommand(delete, connection);
            deleteCommand.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = deleteCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                deleteCommand.ExecuteNonQuery();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "chaarcter deleted");

            }

            reader.Close();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.BadRequest, "character does not exist");
        }

        
        
    }
}
