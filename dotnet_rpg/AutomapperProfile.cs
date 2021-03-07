using AutoMapper;
using dotnet_rpg.Dtos.Character;
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
        }

    }
}
