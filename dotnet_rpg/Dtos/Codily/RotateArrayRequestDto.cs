using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Codily
{
    public class RotateArrayRequestDto
    {
        public int[] arrayOfInts { get; set; }
        public int k { get; set; } //number of rotations
    }
}
