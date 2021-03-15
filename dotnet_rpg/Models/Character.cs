using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public int HitPoints { get; set; } = 10;

        public int Might { get; set; } = 10;

        public int Magic { get; set; } = 10;

        public int Dexterity { get; set; } = 10;

        public RpgClass Class { get; set; } = RpgClass.Apprentice;

        public User User { get; set; }

        public Weapon Weapon { get; set; }// 1: 1 relation

        public List<CharacterSkill> CharacterSkills { get; set; }

        //adding some properties for fight simulation
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }

    }
}
