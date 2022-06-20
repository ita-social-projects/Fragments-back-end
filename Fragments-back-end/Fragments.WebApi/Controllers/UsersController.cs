using Fragments.Domain.Dto;
using Fragments.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO user)
        {
            await _userService.CreateAsync(user);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>>GetUser(int id)
        {
            return await _userService.GetAsync(id); 
        }
    }
}
