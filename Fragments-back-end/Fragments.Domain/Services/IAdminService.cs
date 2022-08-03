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
        Task<IReadOnlyList<User>> GetUsersAsync();
        Task<IEnumerable<User>> Sort(SortDTO sortDTO);
        Task<List<User>> Filter(FilterDTO filterDTO);
        Task AssignRole(RoleDTO roleDTO, int id);

    }
}
