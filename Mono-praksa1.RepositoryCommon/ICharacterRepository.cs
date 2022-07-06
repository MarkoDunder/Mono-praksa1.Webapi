using Mono_praksa1.Common;
using Mono_praksa1.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono_praksa1.RepositoryCommon
{
    public interface ICharacterRepository
    {
          Task<List<Character>> GetAllAsync();
          Task<Character> PostCharacterAsync(Character character);
          Task<Character> UpdateCharacterAsync(int id, Character character);
          Task <bool>DeleteCharacterAsync(int id);

        Task<List<Character>> GetAllAsyncFiltered(Paging paging, Sorting sorting, FilteredCharacter filteredCharacter);


    }
}
