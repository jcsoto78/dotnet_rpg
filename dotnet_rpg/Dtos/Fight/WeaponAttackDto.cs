using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Fight
{
    public class WeaponAttackDto
    {
        // since a character can only have 1 weapon, no weapon Id is needed, opponent and attacker must have a weapon assigned
        public int AttackerId { get; set; }

        public int OpponentId { get; set; }
    }
}
