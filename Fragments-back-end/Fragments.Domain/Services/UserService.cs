using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Models;
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
        private readonly FragmentsContext _context;
        public UserService(FragmentsContext context)
        {
            _context = context;
        }
        public async Task<bool> IsEmailAlreadyExists(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task Create(User user)
        {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
        }
    }
}
