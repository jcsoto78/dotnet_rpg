using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[Controller]")] //routes by controller name matching
    public class CharacterController : ControllerBase
    {
        //adding some mock up data
        private static readonly List<Character> MyCharacters = new List<Character> {
            new Character(),
            new Character { Id = 1, Name = "Rolf", Class = RpgClass.Fighter },
            new Character { Id = 6, Name = "Sam", Class = RpgClass.Mage }
        };

        //action methods

        [HttpGet("GetAll")]
        public IActionResult GetMock()
        {
            return Ok(MyCharacters);
        }

        [HttpGet("{id}")] // id must go inside curly brackets and match id function parameter
        public IActionResult GetCharacher(int id)
        {
            var myCharacter = MyCharacters.FirstOrDefault(c => c.Id == id);

            if (myCharacter != null)
            {
                return Ok(MyCharacters.FirstOrDefault(c => c.Id == id));
            }

            return BadRequest();
      
        }

    }
}
