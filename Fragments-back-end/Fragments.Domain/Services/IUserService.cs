using Fragments.Data.Entities;
using Fragments.Domain.Dto;


namespace Fragments.Domain.Services
{
    public interface IUserService
    {
        public Task<bool> IsEmailAlreadyExistsAsync(string email);
        public Task CreateAsync(UserDTO user);
    }

}
