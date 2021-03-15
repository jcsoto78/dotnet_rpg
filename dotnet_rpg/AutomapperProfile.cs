using AutoMapper;
using dotnet_rpg.Dtos.Character;
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
            CreateMap<Character, GetCharacterDto>(); //every possible mapping nees a createMap profile
            CreateMap<Character, AddCharacterDto>(); //every possible mapping nees a createMap profile
            CreateMap<AddCharacterDto, Character > (); //every possible mapping nees a createMap profile
            CreateMap<AddCharacterDto, GetCharacterDto>(); //every possible mapping nees a createMap profile
            CreateMap<UpdateCharacterDto, Character>(); //every possible mapping nees a createMap profile
            CreateMap<UpdateCharacterDto, GetCharacterDto>(); //every possible mapping nees a createMap profile
            CreateMap<AddWeaponDto, Weapon>(); //every possible mapping nees a createMap profile
            CreateMap<Weapon, GetWeaponDto>(); //every possible mapping nees a createMap profile

        }

    }
}
