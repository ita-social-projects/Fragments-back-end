using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fragments.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("getUsersBySearch")]
        public async Task <IReadOnlyList<UserDTO>> Get(string text)
        {
            return await _adminService.GetUserWithSearchAsync(text);
        }
    }
}
