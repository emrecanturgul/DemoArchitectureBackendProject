using Business.Repositories.OperationClaimRepository;
using Business.Repositories.UserRepository;
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
            var result = _userService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result); 
        }
        [HttpGet("getbyId")] 
        public IActionResult GetById(int userId)
        {
            var result = _userService.GetById(userId);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message); 
        }
        [HttpPost("update")] 
        public IActionResult Update(User user) 
        {
            var result = _userService.Update(user); 
            if(result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(User user)
        {
            var result = _userService.Delete(user);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result);
        }
        [HttpPost("changePassword")]
        public IActionResult ChangePassword(UserChangePasswordDto passwordDto)
        {
            var result = _userService.ChangePassword(passwordDto);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result.Message); 
        }
        

    }   
}
