using Mono_praksa1.Common;
using Mono_praksa1.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono_Praksa1.Service.Common
{
    public interface ICharactersService
    {
        Task<List<Character>> GetCharacters();
        Task<Character> PostCharacter(Character character);

        Task<Character> PutCharacter(int id, Character character);

        Task<bool> DeleteCharacter(int id);

        Task<List<Character>> GetCharactersFiltered(Paging paging, Sorting sorting, FilteredCharacter filteredCharacter);
    }
}
