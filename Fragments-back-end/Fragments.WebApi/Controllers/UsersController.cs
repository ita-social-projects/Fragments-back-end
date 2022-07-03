using Fragments.Domain.Dto;
using Fragments.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fragments.WebApi.Controllers
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
        [HttpGet("get-me"), Authorize]
        public async Task<ActionResult<string>> GetMe()
        {
            var userName = await _userService.GetMe();
            return Ok(userName);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO user)
        {
            await _userService.CreateAsync(user);

            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AuthenticateRequestDTO user)
        {
            var response = await _userService.LoginAsync(user);
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            return await _userService.GetAsync(id);
        }
    }
}
