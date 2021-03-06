﻿using System;
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

    }
}
