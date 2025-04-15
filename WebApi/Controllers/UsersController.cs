using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
            _userService = userService;   
        }
       
        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            return Ok(_userService.GetList());
        }
        [HttpDelete("delete")]
        public IActionResult Delete(LoginAuthDto loginDto)
        {
            _userService.Delete(loginDto);
            return Ok();
        } 


    }
}
