using Fragments.Domain.Services.Interfaces;
using Fragments.Data.Context;
using Fragments.Domain.Dto;
using AutoMapper;
using Fragments.Data.Entities;
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
        public async Task<IReadOnlyList<UserDTO>> GetUserWithSearchAsync(string text)
        {
            var users = _context.Users.Where(s => s.Email!.Contains(text) || s.FullName!.Contains(text))
                .Select(s => new User
                {
                    Email = s.Email,
                    FullName = s.FullName,
                    RepresentativeAuthority = s.RepresentativeAuthority,
                    RepresentativeHEI = s.RepresentativeHEI
                }).OrderBy(x => x.FullName);
            return _mapper.Map<IReadOnlyList<User>, IReadOnlyList<UserDTO>>(await users.ToListAsync());
        }
    }
}
