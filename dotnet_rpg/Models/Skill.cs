using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Skill
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }

        //joining table is needed for many : many entity relations, a list for 1 : many

        public List<CharacterSkill> CharacterSkills { get; set; }

    }
}
