using Mono_praksa1.Model.Models;
using Mono_praksa1.Repository;
using Mono_Praksa1.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Mono_praksa1.Service
{
    public class CharacterService:ICharactersService
    {
        public CharacterService() { }
        public Task<List<Character>> GetCharacters()
        {
            CharacterRepository repository = new CharacterRepository();
            return repository.GetAllAsync();
        }

        public Task PostCharacter(Character character)
        {
            CharacterRepository repository = new CharacterRepository();
            return repository.PostCharacterAsync(character);
        }

        public Task PutCharacter(int id, Character character)
        {
            CharacterRepository repository = new CharacterRepository();
            return repository.UpdateCharacterAsync(id, character);
        }

        public Task DeleteCharacter(int id)
        {
            CharacterRepository repository = new CharacterRepository();
            return repository.DeleteCharacterAsync(id);
        }
    }
}
