using Mono_praksa1.Common;
using Mono_praksa1.Model.Models;
using Mono_praksa1.Repository;
using Mono_praksa1.RepositoryCommon;
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
        protected ICharacterRepository repository;
        public CharacterService(ICharacterRepository repository) {
            this.repository = repository;
        }
        
        public async Task<List<Character>> GetCharacters()
        {
            
            return await  repository.GetAllAsync();
        }

        public async Task<Character> PostCharacter(Character character)
        {
            //CharacterRepository repository = new CharacterRepository();
            return await repository.PostCharacterAsync(character);
        }

        public async Task<Character> PutCharacter(int id, Character character)
        {
            //CharacterRepository repository = new CharacterRepository();
            return  await repository.UpdateCharacterAsync(id, character);
        }

        public async Task<bool> DeleteCharacter(int id)
        {
            //CharacterRepository repository = new CharacterRepository();
            return  await repository.DeleteCharacterAsync(id);
        }

        public async Task<List<Character>> GetCharactersFiltered(Paging paging, Sorting sorting, FilteredCharacter filteredCharacter)
        {
            return await repository.GetAllAsyncFiltered(paging, sorting, filteredCharacter);
        }
    }
}
