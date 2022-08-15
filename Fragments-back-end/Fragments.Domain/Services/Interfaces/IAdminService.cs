using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Services
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
