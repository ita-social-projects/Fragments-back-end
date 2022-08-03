using Fragments.Domain.Dto;
using Fragments.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fragments.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPut("assign-role")]
        public async Task<IActionResult> AssignRole(RoleDTO roleDTO, int id)
        {
            await _adminService.AssignRole(roleDTO, id);
            return Ok();
        }
        [HttpGet("get-all")]
        public IActionResult GetAll(int id)
        {
            return Ok(_adminService.GetUsersAsync());
        }
        [HttpGet("sort")]
        public IActionResult Sort(SortDTO sortDTO)
        {
            return Ok(_adminService.Sort(sortDTO));
        }
        [HttpGet("filter")]
        public IActionResult Filter(FilterDTO filterDTO)
        {
            return Ok(_adminService.Filter(filterDTO));
        }

    }
}
