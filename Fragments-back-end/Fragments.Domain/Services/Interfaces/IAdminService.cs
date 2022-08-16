using Fragments.Domain.Dto;

namespace Fragments.Domain.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IReadOnlyList<AdminDto>> GetUsersAsync();
        Task<IEnumerable<AdminDto>> Sort(SortDto sortDto);
        Task AssignRole(RoleDto roleDto, int id);
        Task<IReadOnlyList<AdminDto>> GetUserWithSearchAsync(FilterAndSearchDto ?filterAndSearchDto);
        Task<IEnumerable<AdminDto>> GetPageAsync(SortDto sortDto, FilterAndSearchDto filterAndSearchDTO, int page);
    }
}
