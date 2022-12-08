using GamesApi.Data;
using GamesApi.Dtos.FightDtos;
using GamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;

        public FightService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var serviceResponse = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if(attacker == null ||opponent == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Can't find a attacker/opponent with that id!";
                    return serviceResponse;
                }
                if (skill == null)
                {

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"{attacker.Name} doesn't know this skill!";
                    return serviceResponse;
                }
                int damage = skill.Damage + new Random().Next(attacker.Intelligence);
                damage -= new Random().Next(opponent.Defence);

                if (damage > 0) opponent.HitPoints -= damage;
                if (opponent.HitPoints <= 0) serviceResponse.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                serviceResponse.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHitPoints = attacker.HitPoints,
                    OpponentHitPoints = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var serviceResponse = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters
                    .Include(c=>c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);
                int damage = attacker.Weapon.Damage + new Random().Next(attacker.Strength);
                damage -= new Random().Next(opponent.Defence);

                if (damage > 0) opponent.HitPoints -= damage;
                if (opponent.HitPoints <= 0) serviceResponse.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                serviceResponse.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHitPoints = attacker.HitPoints,
                    OpponentHitPoints = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
