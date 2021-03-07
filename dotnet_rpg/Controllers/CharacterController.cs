using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
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
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        //action methods

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_characterService.GetAllCharacters());
        }

        [HttpGet("{id}")] // id must go inside curly brackets and match id function parameter
        public IActionResult GetCharacher(int id)
        {
            var myCharacter = _characterService.GetCharacterById(id);

            if (myCharacter != null)
            {
                return Ok(myCharacter);
            }

            return BadRequest();
      
        }

    }
}
