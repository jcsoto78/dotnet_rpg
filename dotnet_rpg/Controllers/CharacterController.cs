using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Controllers
{
    [Authorize]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacterById(id);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = await _characterService.AddCharacter(newCharacter);

            if (response.Success)
            {
                return Ok(response);
            }

            return Conflict(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            int securityUserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _characterService.GetAllCharacters(securityUserId));
        }


        [AllowAnonymous] //skips authentication
        [HttpGet("{id}")] // id must go inside curly brackets and match id function parameter
        public async Task<IActionResult> GetCharacher(int id)
        {
            var myCharacter = await _characterService.GetCharacterById(id);

            if (myCharacter.Data != null)
            {
                return Ok(myCharacter);
            }

            return BadRequest();
      
        }

    }
}
