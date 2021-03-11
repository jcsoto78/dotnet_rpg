using dotnet_rpg.Data;
using dotnet_rpg.Dtos.User;
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
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository) //DI requires a new registration at Startup
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto registerRequest) 
        {
            var response = await _authRepository.Register(
                new User { Username = registerRequest.Username}, registerRequest.Password);

            if (response.Success)
            {
                return Ok(response);
            }
            else 
            {
                return BadRequest(response);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto loginRequest)
        {
            var response = await _authRepository.Login(loginRequest.Username, loginRequest.Password);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
