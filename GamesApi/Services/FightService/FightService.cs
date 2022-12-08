using AutoMapper;
using GamesApi.Data;
using GamesApi.Dtos.FightDtos;
using GamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var serviceResponse = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };
            
            try
            {
                var characters = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .Where(c => request.CharacterIds.Contains(c.Id)).ToListAsync();

                Boolean defeated = false;
                while (!defeated)
                {
                    foreach(var attacker in characters)
                    {
                        var opponents = characters.Where(c=>c.Id != attacker.Id).ToList();
                        var index = new Random().Next(opponents.Count);
                        var opponent = opponents[index]; //generate a random opponent

                        int damage = 0;
                        string attackUsed = string.Empty;

                        bool useWeapon = new Random().Next(2) == 0;

                        if (useWeapon) //if the line 39 equals 0
                        {
                            attackUsed = attacker.Weapon.Name;

                            damage = attacker.Weapon.Damage + new Random().Next(attacker.Strength);
                            damage -= new Random().Next(opponent.Defence);

                            if (damage > 0) opponent.HitPoints -= damage;

                        }
                        else
                        {
                            var index1 = new Random().Next(attacker.Skills.Count);
                            var skill = attacker.Skills[index1];
                            attackUsed = skill.Name;
                            damage = skill.Damage + new Random().Next(attacker.Intelligence);
                            damage -= new Random().Next(opponent.Defence);

                            if (damage > 0) opponent.HitPoints -= damage;
                        }
                        serviceResponse.Data.Log
                           .Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage.");
                        if (opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            serviceResponse.Data.Log.Add($"{opponent.Name} has been defeated!");
                            serviceResponse.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
                            break;
                        }
                    }
                }
                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<HighScoreDto>>> GetHighScore()
        {
            var serviceResponse = new ServiceResponse<List<HighScoreDto>>();
            var characters = await _context.Characters
               .Where(c => c.Fights > 0)
               .OrderByDescending(c => c.Victories)
               .ThenBy(c => c.Defeats)
               .ToListAsync();

            serviceResponse.Data = characters.Select(c => _mapper.Map<HighScoreDto>(c)).ToList();
    
            return serviceResponse;
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
