using Fragments.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Services
{
    public interface IUserService
    {
        public Task<bool> IsEmailAlreadyExists(string email);
        public Task Create(User user);
        public Task<List<User>> GetUsers();
    }

}
