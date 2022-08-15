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
        Task<IEnumerable<AdminDto>> Sort(SortDTO sortDTO);
        Task AssignRole(RoleDto roleDTO, int id);
        Task<IReadOnlyList<AdminDto>> GetUserWithSearchAsync(FilterAndSearchDTO ?filterAndSearchDTO);
        Task<IEnumerable<AdminDto>> GetPageAsync(SortDTO sortDTO, FilterAndSearchDTO filterAndSearchDTO, int page);
    }
}
