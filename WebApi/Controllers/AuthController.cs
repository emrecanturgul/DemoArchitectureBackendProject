using Business.Authentication;
using Business.Repositories.OperationClaimRepository;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
        [ApiController]
        public class AuthController : ControllerBase
        {
            private readonly IAuthService _authService;

            public AuthController(IAuthService authService)
            {
                _authService = authService;
            }
            [HttpPost("register")]
            public IActionResult Register([FromForm]RegisterAuthDto authDto)
            {
                var result = _authService.Register(authDto);
                if(result.Success == true)
                {
                return Ok(result);
                }
                return BadRequest(result.Message); 
                
            }
            [HttpPost("login")] 
            public IActionResult Login(LoginAuthDto loginAuthDto)
            {
                var result = _authService.Login(loginAuthDto);
                return Ok(result);      
            }
        }
    }
