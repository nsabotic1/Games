using GamesApi.Dtos.SkillDtos;
using GamesApi.Dtos.WeaponDtos;
using GamesApi.Models;

namespace GamesApi.Dtos.CharacterDtos
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int HitPoints { get; set; }
        public int Strength { get; set; } 
        public int Defence { get; set; } 
        public int Intelligence { get; set; }
        public RpgClass Class { get; set; }
        public GetWeaponDto Weapon { get; set; }

        public List<GetSkillDto> Skills { get; set; }
    }
}
