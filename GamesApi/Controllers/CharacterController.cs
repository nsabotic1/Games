using GamesApi.Dtos.CharacterDtos;
using GamesApi.Models;
using GamesApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacter());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetById(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Add(AddCharacterDto character)
        {
            
            return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Update(UpdateCharacterDto character)
        {
            var serviceReponse = await _characterService.UpdateCharacter(character);
            if (serviceReponse.Data == null)
            {
                return NotFound(serviceReponse);
            }
            return Ok(serviceReponse);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
        {
            var serviceReponse = await _characterService.DeleteCharacter(id);
            if (serviceReponse.Data == null)
            {
                return NotFound(serviceReponse);
            }
            return Ok(serviceReponse);
        }

    }
}
