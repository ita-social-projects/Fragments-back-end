using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;
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
        public async Task<IActionResult> AssignRole(RoleDto roleDto, int id)
        {
            await _adminService.AssignRole(roleDto, id);
            return Ok();
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _adminService.GetUsersAsync()); 
        }
        [HttpGet("sort")]
        public async Task<IActionResult> Sort([FromQuery]SortDto sortDto)
        {
            return Ok(await _adminService.Sort(sortDto));
        }
        [HttpGet("getUsersBySearch")]
        public async Task<IReadOnlyList<AdminDto>> getSearchAsync([FromQuery]FilterAndSearchDto ?filterAndSearchDto)
        {
            return await _adminService.GetUserWithSearchAsync(filterAndSearchDto);
        }
        [HttpGet("get-page")]
        public async Task<IEnumerable<AdminDto>> getPage([FromQuery] FilterAndSearchDto? filterAndSearchDTO,
            [FromQuery] SortDto sortDto,
            int page)
        {
            return await _adminService.GetPageAsync(sortDto, filterAndSearchDTO!, page);
        }

    }
}
