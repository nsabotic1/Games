using GamesApi.Dtos.CharacterDtos;
using GamesApi.Dtos.WeaponDtos;
using GamesApi.Models;

namespace GamesApi.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}
