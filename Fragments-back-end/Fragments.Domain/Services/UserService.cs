using AutoMapper;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fragments.Domain.Services
{
    public class UserService: IUserService
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

        public async Task<UserDTO> GetAsync(int id)
        {

            var user = await _context.Users.FindAsync(id);
            var userInfo = _mapper.Map<UserDTO>(user);

            if (userInfo == null)
            {
                throw new Exception("Not Found");
            }

            return userInfo;
        }
    }
}
