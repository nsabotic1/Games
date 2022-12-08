using GamesApi.Dtos.FightDtos;
using GamesApi.Models;

namespace GamesApi.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
    }
}
