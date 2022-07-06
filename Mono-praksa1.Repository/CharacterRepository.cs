using Mono_praksa1.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Http;
using Azure.Core;
using Mono_praksa1.RepositoryCommon;
using Mono_praksa1.Common;
using System.Text;

namespace Mono_praksa1.Repository
{
    public class CharacterRepository:ICharacterRepository
    {
        public CharacterRepository() { }
        
        public string connectionString = @"Data Source=DESKTOP-RN0M5JB;Initial Catalog=master;Integrated Security=True";
        public async Task<List<Character>> GetAllAsync()
        {
            List<Character> characters = new List<Character>();
            SqlConnection connection = new SqlConnection(connectionString);
            string fetchAll = "Select * from dbo.Characters";
            SqlCommand get = new SqlCommand(fetchAll, connection);
            SqlDataReader reader = await get.ExecuteReaderAsync();
            connection.Open();
            while (reader.HasRows)
            {
                characters.Add(new Character { Id = reader.GetInt32(0), Name = reader.GetString(1), Surname = reader.GetString(2) });
            }
            return characters;
        }

        public async Task<Character> PostCharacterAsync(Character character)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string insertCharacter = "Insert into dbo.Characters(Id, Name, Surname) Values(@Id, @Name, @Surname); ";
            SqlCommand command = new SqlCommand(insertCharacter, connection);
            command.Parameters.AddWithValue("@Id", character.Id);
            command.Parameters.AddWithValue("@Name", character.Name);
            command.Parameters.AddWithValue("@Surname", character.Surname);
            Character postedCharacter = new Character(character.Id, character.Name, character.Surname);

            string selector = "Select Id From dbo.Characters Where Id=@Id;";
            SqlCommand searchCommand = new SqlCommand(selector, connection);
            searchCommand.Parameters.AddWithValue("@Id", character.Id);

            connection.Open();
            SqlDataReader reader =  await searchCommand.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                reader.Close();
                connection.Close();
                return postedCharacter;

            }
            else
            {
                reader.Close();
                //SqlDataReader reader1 = command.ExecuteReader();
                command.ExecuteNonQuery();
                return postedCharacter;
            }
            
        }

        public async Task<Character> UpdateCharacterAsync(int id, [FromBody] Character character)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string updateCharacter = "Update dbo.Characters Set Id=@id, Name=@Name, Surname=@Surname Where Id=@Id; ";
            SqlCommand updatecmd = new SqlCommand(updateCharacter, connection);
            updatecmd.Parameters.AddWithValue("@Id", id);
            updatecmd.Parameters.AddWithValue("@Name", character.Name);
            updatecmd.Parameters.AddWithValue("@Surname", character.Surname);
            Character updatedCharacter = new Character(id, character.Name, character.Surname);
            connection.Open();
            SqlDataReader reader = await updatecmd.ExecuteReaderAsync();
            if (reader.HasRows)
            {

                reader.Close();
                updatecmd.ExecuteNonQuery();
                connection.Close();


            }
            return updatedCharacter;
        }

        public async Task<bool> DeleteCharacterAsync(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string delete = "Delete from dbo.Characters where Id=@Id";
            SqlCommand deleteCommand = new SqlCommand(delete, connection);
            deleteCommand.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = await deleteCommand.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                reader.Close();
                deleteCommand.ExecuteNonQuery();
                connection.Close();
                return true;

            }

            reader.Close();
            connection.Close();
            return false;
        }

        public async Task<List<Character>> GetAllAsyncFiltered(Paging paging, Sorting sorting, FilteredCharacter filteredCharacter)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<Character> characters = new List<Character>();
            SqlCommand getCommand = new SqlCommand();
            getCommand.Connection = connection;
            StringBuilder stringBuilder = new StringBuilder("Select * from dbo.Characters where 1=1");
            connection.Open();
            if (filteredCharacter.Name != null)
            {
                stringBuilder.Append("and Name Like @Name");
                getCommand.Parameters.AddWithValue("@Name", filteredCharacter.Name);

            }

            if (filteredCharacter.Name != null)
            {
                stringBuilder.Append("and Surname Like @Surame");
                getCommand.Parameters.AddWithValue("@Surname", filteredCharacter.Surname);

            }
            
            stringBuilder.Append(string.Format("Order by {0} {1} ", sorting.OrderBy, sorting.SortOrder));
            stringBuilder.Append("offset @offset rows fetch next @resultsPerPage rows only ;");
            int offset = (paging.PageNumber - 1) * paging.ResultsPerPage;
            getCommand.Parameters.AddWithValue("@offset", offset);
            getCommand.Parameters.AddWithValue("@resultsPerPage", paging.ResultsPerPage);

            getCommand.CommandText = stringBuilder.ToString();
            SqlDataReader reader = await getCommand.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while(await reader.ReadAsync())
                {
                    characters.Add(new Character(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
            }
            reader.Close();
            connection.Close();
            return characters;

        }



    }
}
