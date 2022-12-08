using AutoMapper;
using GamesApi.Dtos.CharacterDtos;
using GamesApi.Dtos.SkillDtos;
using GamesApi.Dtos.WeaponDtos;
using GamesApi.Models;

namespace GamesApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<UpdateCharacterDto, Character>()
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<RpgClass?, RpgClass>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<AddWeaponDto, Weapon>();
            CreateMap<Weapon, GetWeaponDto>();

            CreateMap<Skill, GetSkillDto>();
        }
    }
}
