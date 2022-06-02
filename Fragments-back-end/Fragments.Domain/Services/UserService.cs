using AutoMapper;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Services
{
    public class UserService:IUserService
    {
        private readonly IFragmentsContext _context;
        private readonly IMapper _mapper;
        public UserService(IFragmentsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> IsEmailAlreadyExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }

        public async Task CreateAsync(UserDTO user)
        {
            var userInfo = _mapper.Map<User>(user);

            await _context.Users.AddAsync(userInfo);

            await _context.SaveChangesAsync();
        }
    }
}
