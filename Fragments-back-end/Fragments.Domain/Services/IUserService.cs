using Fragments.Domain.Dto;

namespace Fragments.Domain.Services
{
    public interface IUserService
    {
        Task<bool> IsEmailAlreadyExistsAsync(string email);
        Task CreateAsync(UserDTO user);
        Task<AuthenticateResponseDTO> LoginAsync(AuthenticateRequestDTO model);
        Task<UserDTO> GetMe();
        Task<UserDTO> GetByIdAsync(int id);
    }
}
