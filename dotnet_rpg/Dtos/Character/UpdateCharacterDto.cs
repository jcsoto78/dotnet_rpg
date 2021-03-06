using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Character
{
    public class UpdateCharacterDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public int HitPoints { get; set; } = 10;

        public int Might { get; set; } = 10;

        public int Magic { get; set; } = 10;

        public int Dexterity { get; set; } = 10;

        public RpgClass Class { get; set; } = RpgClass.Apprentice;
    }
}
