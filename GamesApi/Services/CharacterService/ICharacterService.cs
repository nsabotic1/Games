using GamesApi.Dtos.CharacterDtos;
using GamesApi.Models;

namespace GamesApi.Services
{
    public interface ICharacterService
    {
        public Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter();
        public Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        public Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        public Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
        public Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}
