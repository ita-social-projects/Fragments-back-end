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
    public class AdminService : IAdminService
    {
        private readonly IFragmentsContext _context;
        private readonly IMapper _mapper;
       
        public AdminService(IFragmentsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
   
        public async Task<IReadOnlyList<AdminDTO>> GetUsersAsync()
        {
            var users = _context.Users.Select(u => new User
            {
                FullName = u.FullName,
                Email = u.Email,
                Birthday = u.Birthday,
                RepresentativeAuthority = u.RepresentativeAuthority,
                RepresentativeHEI = u.RepresentativeHEI,
                UsersRole = u.UsersRole,
                Id = u.Id
            }) ;

            return _mapper.Map<IReadOnlyList<User>, IReadOnlyList<AdminDTO>>(await users.ToListAsync());
        }
   
        public async Task<IEnumerable<AdminDTO>> Sort(SortDTO sortDTO)
        {
            var users = _context.Users.Select(u => new User
            {
                FullName = u.FullName,
                Email = u.Email,
                Birthday = u.Birthday,
                RepresentativeAuthority = u.RepresentativeAuthority,
                RepresentativeHEI = u.RepresentativeHEI,
                UsersRole = u.UsersRole,
                Id = u.Id
            });
            if (sortDTO !=null)
            {
                switch (sortDTO.PropertyName, sortDTO.IsAscending)
                {
                    case ("FullName",true):
                        users = users.OrderBy(u => u.FullName);
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDTO>>(await users.ToListAsync());

                    case ("FullName", false):
                        users = users.OrderByDescending(u => u.FullName);
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDTO>>(await users.ToListAsync());

                    case ("Email", true):
                        users = users.OrderBy(u => u.Email);
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDTO>>(await users.ToListAsync());

                    case ("Email", false):
                        users = users.OrderByDescending(u => u.Email);
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDTO>>(await users.ToListAsync());

                    default:
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDTO>>(await users.ToListAsync());
                }
            }
            else
            {
                return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDTO>>(await users.ToListAsync());
            }
        }
        public async Task<IReadOnlyList<AdminDTO>> GetUserWithSearchAsync(FilterAndSearchDTO ?filterAndSearchDTO)
        {
            var splited = filterAndSearchDTO!.SearchText.Split(" ");

            var users = _context.Users
                .AsAsyncEnumerable()
                //.AsEnumerable()
                .Where(string.IsNullOrEmpty(filterAndSearchDTO?.SearchText)
                    ? (s => s.Id != 0)
                    : (s => splited.All(x => s.Email!.Contains(x) || s.FullName!.Contains(x))))
                .Where(q =>
                (filterAndSearchDTO!.RepresentativeHEI
                    ? q.RepresentativeHEI
                    : q.RepresentativeAuthority || !q.RepresentativeAuthority)
                    && (filterAndSearchDTO!.RepresentativeAuthority
                    ? q.RepresentativeAuthority
                    : q.RepresentativeHEI || !q.RepresentativeHEI)
                )
            .Select(s => new User
            {
                        Email = s.Email,
                        FullName = s.FullName,
                        RepresentativeAuthority = s.RepresentativeAuthority,
                        RepresentativeHEI = s.RepresentativeHEI,
                        UsersRole = s.UsersRole,
                        Id = s.Id,
                        Birthday = s.Birthday

            });   
                 return  _mapper.Map<IReadOnlyList<User>, IReadOnlyList<AdminDTO>>(await users.ToListAsync());
        }
        //public bool ContainsThis( User str1, string values)
        //{
        //    var str = str1.Email!;

        //    var stp = values.Split(" ");
        //    if (stp.Length > 0)
        //    {
        //        foreach (string value in stp)
        //        {
        //            if (str.Contains(value))
        //                return false;
        //        }
        //    }

        //    return true;
        //}
    }

    //public static class StringExtensions
    //{
    //    public static bool ContainsAll(this List<User> str, string values)
    //    {
    //        var stp = values.Split(' ');
    //        if (stp.Length > 0)
    //        {
    //            foreach (string value in stp)
    //            {
    //                if (str.Contains(stp) == false)
    //                    return false;
    //            }
    //        }

    //        return true;
    //    }
    //}
}
