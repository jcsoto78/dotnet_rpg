using AutoMapper;
using dotnet_rpg.Dtos;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Character, GetCharacterDto>() //every possible mapping nees a createMap profile
                .ForMember(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
            CreateMap<Character, AddCharacterDto>(); //every possible mapping nees a createMap profile
            CreateMap<AddCharacterDto, Character > (); //every possible mapping nees a createMap profile
            CreateMap<AddCharacterDto, GetCharacterDto>(); //every possible mapping nees a createMap profile
            CreateMap<UpdateCharacterDto, Character>(); //every possible mapping nees a createMap profile
            CreateMap<UpdateCharacterDto, GetCharacterDto>(); //every possible mapping nees a createMap profile
            CreateMap<AddWeaponDto, Weapon>(); //every possible mapping nees a createMap profile
            CreateMap<Weapon, GetWeaponDto>(); //every possible mapping nees a createMap profile
            CreateMap<Skill, GetSkillDto>(); //every possible mapping nees a createMap profile
            CreateMap<GetSkillDto, Skill>(); //every possible mapping nees a createMap profile
            CreateMap<Character, HighscoreDto >(); //every possible mapping nees a createMap profile


        }

    }
}
