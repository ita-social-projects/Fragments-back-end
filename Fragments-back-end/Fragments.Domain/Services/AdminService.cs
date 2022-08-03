using AutoMapper;
using Castle.Core.Configuration;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Services
{
    internal class AdminService : IAdminService
    {
        private readonly IFragmentsContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(IFragmentsContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IConfiguration configiguration)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configiguration;
        }

        public async Task AssignRole(RoleDTO roleDTO, int id)
        {
            var existingUser = _context.Users
           .Where(p => p.Id == id)
           .Include(r => r.UsersRole)
           .SingleOrDefault();
            if (existingUser != null)
            {
                foreach (var existingChannel in existingUser.UsersRole.ToList())
                {
                    if (!roleDTO.UsersRole.Any(r => r.RoleId == existingChannel.RoleId))
                    {
                        _context.UsersRoles.Remove(existingChannel);
                    }

                }
                foreach (var role in roleDTO.UsersRole)
                {
                    if (role.RoleId != null)
                    {
                        var existingRole = existingUser.UsersRole
                        .Where(r => r.RoleId == role.RoleId)
                        .SingleOrDefault();
                        if (existingRole != null)
                        {
                            _context.Entry(existingRole).CurrentValues.SetValues(role);
                        }
                    }
                    else
                    {

                        var newRole = new UsersRole
                        {
                            RoleId = role.RoleId,
                            Role = role.Role,
                            User = role.User,
                            UserId = role.UserId,
                        };
                        existingUser.UsersRole.Add(newRole);
                    }
                }

                await _context.SaveChangesAsync();

            }
        }
        public async Task<IReadOnlyList<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        //IEnumerable     
        public async Task<IEnumerable<User>> Sort(SortDTO sortDTO)
        {
            if (sortDTO !=null)
            {
                switch (sortDTO.PropertyName, sortDTO.IsAscending)
                {
                    case ("FullName",true):
                        return  _context.Users.OrderBy(u => u.FullName);
                        
                    case ("FullName", false):
                        return  _context.Users.OrderByDescending(u=>u.FullName);

                    case ("Email", true):
                        return  _context.Users.OrderBy(u => u.Email);

                    case ("Email", false):
                        return  _context.Users.OrderByDescending(u => u.Email);
                    
                    default:
                        return await _context.Users.ToListAsync();
                }
            }
            else
            {
                return await _context.Users.ToListAsync();
            }
        }
        public async Task<List<User>> Filter(FilterDTO filterDTO)
        {
            return await _context.Users.Where(user => user.UsersRole.Any(urm => filterDTO.roles.Contains(urm.Role.RoleName))).ToListAsync();
        }
    }
}
