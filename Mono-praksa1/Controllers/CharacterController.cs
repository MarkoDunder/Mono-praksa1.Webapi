
using Mono_praksa1.Service;
using System.Net.Http;
using System.Web.Http;

namespace Mono_praksa1.Controllers
{
    public class CharacterController : ApiController
    {

        
        
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            CharacterService service = new CharacterService();
            service.GetCharacters();
            return Request.CreateResponse();
        }

        [HttpPost]
        public HttpResponseMessage PostCharacter([FromBody] Model.Models.Character character)
        {
            CharacterService service = new CharacterService();
            service.PostCharacter(character);
            return Request.CreateResponse();
        }

        [HttpPut]
        public HttpResponseMessage UpdateCharacter(int id, [FromBody] Model.Models.Character character)
        {
            CharacterService service = new CharacterService();
            service.PutCharacter(id, character);
            return Request.CreateResponse();
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCharacter(int id)
        {
            CharacterService service = new CharacterService();
            service.DeleteCharacter(id);
            return Request.CreateResponse();
        }
    }
}
        
            
         
    
