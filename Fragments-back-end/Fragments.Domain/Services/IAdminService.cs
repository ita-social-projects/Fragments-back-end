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
        Task<IReadOnlyList<AdminDTO>> GetUsersAsync();
        Task<IEnumerable<AdminDTO>> Sort(SortDTO sortDTO);
        Task AssignRole(RoleDTO roleDTO, int id);
        Task<IReadOnlyList<AdminDTO>> GetUserWithSearchAsync(FilterAndSearchDTO ?filterAndSearchDTO);
    }
}
