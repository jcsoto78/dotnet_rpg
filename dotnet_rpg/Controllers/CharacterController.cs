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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")] // id must go inside curly brackets and match id function parameter
        public async Task<IActionResult> GetCharacher(int id)
        {
            var myCharacter = await _characterService.GetCharacterById(id);

            if (myCharacter != null)
            {
                return Ok(myCharacter);
            }

            return BadRequest();
      
        }

    }
}
