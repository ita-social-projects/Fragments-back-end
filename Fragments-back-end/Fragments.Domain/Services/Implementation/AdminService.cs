using AutoMapper;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Fragments.Domain.Services.Implementation
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
        public async Task AssignRole(RoleDto roleDto, int id)
        {
            var existingUser = _context.Users
           .Where(p => p.Id == id)
           .Include(r => r.UsersRole)
           .SingleOrDefault();
            if (existingUser != null)
            {
                foreach (var existingChannel in existingUser.UsersRole.ToList())
                {
                    if (!roleDto.UsersRole.Any(r => r.RoleId == existingChannel.RoleId))
                    {
                        _context.UsersRoles.Remove(existingChannel);
                    }

                }
                foreach (var role in roleDto.UsersRole)
                {
                    if (role.RoleId != 0)
                    {
                        var existingRole = existingUser.UsersRole
                        .SingleOrDefault(r => r.RoleId == role.RoleId);
                        SetRoleValues(existingRole!, role);
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
   
        public async Task<IReadOnlyList<AdminDto>> GetUsersAsync()
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

            return _mapper.Map<IReadOnlyList<User>, IReadOnlyList<AdminDto>>(await users.ToListAsync());
        }
   
        public async Task<IEnumerable<AdminDto>> Sort(SortDto sortDto)
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
            if (sortDto != null)
            {
                switch (sortDto.PropertyName, sortDto.IsAscending)
                {
                    case ("FullName",true):
                        users = users.OrderBy(u => u.FullName);
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDto>>(await users.ToListAsync());

                    case ("FullName", false):
                        users = users.OrderByDescending(u => u.FullName);
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDto>>(await users.ToListAsync());

                    case ("Email", true):
                        users = users.OrderBy(u => u.Email);
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDto>>(await users.ToListAsync());

                    case ("Email", false):
                        users = users.OrderByDescending(u => u.Email);
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDto>>(await users.ToListAsync());

                    default:
                        return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDto>>(await users.ToListAsync());
                }
            }
            else
            {
                return _mapper.Map<IReadOnlyList<User>, IEnumerable<AdminDto>>(await users.ToListAsync());
            }
        }
        public async Task<IReadOnlyList<AdminDto>> GetUserWithSearchAsync(FilterAndSearchDto ?filterAndSearchDto)
        {
            var users = _context.Users
                .Where(string.IsNullOrEmpty(filterAndSearchDto?.SearchText)
                ? (s => s.Id != 0)
                : (s => s.Email!.Contains(filterAndSearchDto.SearchText) || s.FullName!.Contains(filterAndSearchDto.SearchText)))
                .Where(q =>
                (filterAndSearchDto!.RepresentativeHEI
                ? q.RepresentativeHEI
                : q.RepresentativeAuthority || !q.RepresentativeAuthority)
                && (filterAndSearchDto!.RepresentativeAuthority
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

            return _mapper.Map<IReadOnlyList<User>, IReadOnlyList<AdminDto>>(await users.ToListAsync());
        }

        public async Task<IEnumerable<AdminDto>> GetPageAsync(SortDto sortDto,FilterAndSearchDto filterAndSearchDTO,int page)
        {
            const int pageSize = 25;

            var users = _context.Users
               .Where(string.IsNullOrEmpty(filterAndSearchDTO?.SearchText)
               ? (s => s.Id != 0)
               : (s => s.Email!.Contains(filterAndSearchDTO.SearchText) || s.FullName!.Contains(filterAndSearchDTO.SearchText)))
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
           if (sortDto != null)
           {
               switch (sortDto.PropertyName, sortDto.IsAscending)
               {
                    case ("FullName", true):
                        users = users.OrderBy(u => u.FullName);
                        break;

                    case ("FullName", false):
                        users = users.OrderByDescending(u => u.FullName);
                        break;

                    case ("Email", true):
                        users = users.OrderBy(u => u.Email);
                        break;

                    case ("Email", false):
                        users = users.OrderByDescending(u => u.Email);
                        break;

                    default:
                        break;
               }
           }

            return _mapper.Map<IReadOnlyList<User>, IReadOnlyList<AdminDto>>(await users
                .Skip((page- 1) * pageSize).Take(pageSize).ToListAsync());

            
        }

        public bool SetRoleValues(UsersRole existingRole, UsersRole role)
        {
            if (existingRole != null)
            {
                _context.Entry(existingRole).CurrentValues.SetValues(role);
            }
            return true;
        }

    }
}
