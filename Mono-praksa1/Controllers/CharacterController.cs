
using Mono_praksa1.Service;
using Mono_Praksa1.Service.Common;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Mono_praksa1.Model.Models;
using System.Threading.Tasks;
using Mono_praksa1.Common;

namespace Mono_praksa1.Controllers
{
    public class CharacterController : ApiController
    {
        protected ICharactersService service;
        public CharacterController(ICharactersService service)
        {
            this.service = service;
        }

        // GET: api/Character
        //[HttpGet]

        //public async Task<HttpResponseMessage> GetAll()
        //{

        //    List<Character> characters = new List<Character>();
        //    characters= await service.GetCharacters();
        //    if (characters == null)
        //    {
        //        return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, "Database has no entries");
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(System.Net.HttpStatusCode.OK, characters);
        //    }
        //}

        [HttpPost]
        public async Task<HttpResponseMessage> PostCharacter([FromBody] Model.Models.Character character)
        {
            //CharacterService service = new CharacterService();
            Character postedCharacter = new Character();
            postedCharacter= await service.PostCharacter(character);
            if (postedCharacter == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Could not post character");
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, postedCharacter);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateCharacter(int id, [FromBody] Model.Models.Character character)
        {
            //CharacterService service = new CharacterService();
            Character updatedCharacter = new Character();
            updatedCharacter= await service.PutCharacter(id, character);
            if (updatedCharacter == null)
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, "There is no character with that id");

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Character updated");
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteCharacter(int id)
        {
            //CharacterService service = new CharacterService();
            bool result= await service.DeleteCharacter(id);
            if (result == false)
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Character with given id does not exist");

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Character deleted");
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetCharactersAsyncFiltered(Paging paging, Sorting sorting, FilteredCharacter filteredCharacter)
        {
            List<Character> characters = new List<Character>();
            characters = await service.GetCharactersFiltered(paging, sorting, filteredCharacter);
            if (characters == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NoContent, "there are no characters");
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, characters);
            }
        }
    }
}
        
            
         
    
