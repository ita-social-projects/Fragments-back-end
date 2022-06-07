using Fragments.Data.Entities;
using Fragments.Domain.Dto;

namespace Fragments.Domain.Services
{
    public interface IUserService
    {
        Task<bool> IsEmailAlreadyExistsAsync(string email);
        Task CreateAsync(UserDTO user);
        Task<AuthenticateResponseDTO> LoginAsync(AuthenticateRequestDTO model);
        string GetMe();
        Task<User?> GetById(int id);
    }
}
