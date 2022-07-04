using Mono_praksa1.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Http;

using System.Net.Http;
using Azure.Core;

namespace Mono_praksa1.Repository
{
    public class CharacterRepository
    {
        public CharacterRepository() { }
        
        public string connectionString = @"Data Source=DESKTOP-RN0M5JB;Initial Catalog=master;Integrated Security=True";
        public async Task<List<Character>> GetAllAsync()
        {
            List<Character> characters = new List<Character>();
            SqlConnection connection = new SqlConnection(connectionString);
            string fetchAll = "Select * from Characters";
            SqlCommand get = new SqlCommand(fetchAll, connection);
            SqlDataReader reader = await get.ExecuteReaderAsync();
            connection.Open();
            while (reader.HasRows)
            {
                characters.Add(new Character { Id = reader.GetInt32(0), Name = reader.GetString(1), Surname = reader.GetString(2) });
            }
            return characters;
        }

        public async Task PostCharacterAsync(Character character)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string insertCharacter = "Insert into Characters(Id, Name, Surname) Values(@Id, @Name, @Surname); ";
            SqlCommand command = new SqlCommand(insertCharacter, connection);
            command.Parameters.AddWithValue("@Id", character.Id);
            command.Parameters.AddWithValue("@Name", character.Name);
            command.Parameters.AddWithValue("@Surname", character.Surname);

            string selector = "Select Id From Characters Where Id=@Id;";
            SqlCommand searchCommand = new SqlCommand(selector, connection);
            searchCommand.Parameters.AddWithValue("@Id", character.Id);

            connection.Open();
            SqlDataReader reader =  await searchCommand.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                reader.Close();
                connection.Close();
                

            }
            else
            {
                reader.Close();
                //SqlDataReader reader1 = command.ExecuteReader();
                command.ExecuteNonQuery();
            }
        }

        public async Task UpdateCharacterAsync(int id, [FromBody] Character character)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string updateCharacter = "Update Characters Set Id=@id, Name=@Name, Surname=@Surname Where Characters.Id=@Id; ";
            SqlCommand updatecmd = new SqlCommand(updateCharacter, connection);
            updatecmd.Parameters.AddWithValue("@Id", id);
            updatecmd.Parameters.AddWithValue("@Name", character.Name);
            updatecmd.Parameters.AddWithValue("@Surname", character.Surname);
            connection.Open();
            SqlDataReader reader = await updatecmd.ExecuteReaderAsync();
            if (reader.HasRows)
            {

                reader.Close();
                updatecmd.ExecuteNonQuery();
                connection.Close();


            }
        }

        public async Task DeleteCharacterAsync(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string delete = "Delete from Characters where Characters.Id=@Id";
            SqlCommand deleteCommand = new SqlCommand(delete, connection);
            deleteCommand.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = await deleteCommand.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                reader.Close();
                deleteCommand.ExecuteNonQuery();
                connection.Close();
                

            }

            reader.Close();
            connection.Close();
            
        }
    }
}
