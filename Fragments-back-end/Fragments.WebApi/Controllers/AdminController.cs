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
        public async Task<IActionResult> AssignRole(RoleDto roleDTO, int id)
        {
            await _adminService.AssignRole(roleDTO, id);
            return Ok();
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _adminService.GetUsersAsync()); 
        }
        [HttpGet("sort")]
        public async Task<IActionResult> Sort([FromQuery]SortDTO sortDTO)
        {
            return Ok(await _adminService.Sort(sortDTO));
        }
        [HttpGet("getUsersBySearch")]
        public async Task<IReadOnlyList<AdminDto>> getSearchAsync([FromQuery]FilterAndSearchDTO ?filterAndSearchDTO)
        {
            return await _adminService.GetUserWithSearchAsync(filterAndSearchDTO);
        }
        [HttpGet("get-page")]
        public async Task<IEnumerable<AdminDto>> getPage([FromQuery] FilterAndSearchDTO? filterAndSearchDTO,
            [FromQuery] SortDTO sortDTO,
            int page)
        {
            return await _adminService.GetPageAsync(sortDTO, filterAndSearchDTO!, page);
        }

    }
}
