using Fragments.Domain.Dto;

namespace Fragments.Domain.Services
{
    public interface IUserService
    {
        Task<bool> IsEmailAlreadyExistsAsync(string email);
        Task CreateAsync(UserDto user);
        Task<AuthenticateResponseDto> LoginAsync(AuthenticateRequestDto model);
        Task<UserDto> GetMe();
        Task<UserDto> GetByIdAsync(int id);
    }
}
