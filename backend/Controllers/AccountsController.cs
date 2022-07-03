using backend.DTOs.Requests;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IDbService _dbService;
        public AccountsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterRequest model)
        {
            var c = await _dbService.PostRegisterUserAsync(model);
            if (c)
                return Ok();
            else return BadRequest();
        }
       
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var c = await _dbService.LoginAsync(loginRequest);
            if (c.accessToken == "1")
                return BadRequest("User doesn't exist");
            else if (c.accessToken == "2")
            {
                return Unauthorized();
            }
            else 
                return Ok(c);
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromHeader(Name = "Authorization")] string token, RefreshTokenRequest refreshToken)
        {
            return Ok(await _dbService.NewTokenAsync(token,refreshToken));
        }
        
        

    }
}

