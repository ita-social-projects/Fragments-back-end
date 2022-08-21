using Fragments.Data.Entities;
using Fragments.Domain.Dto;

namespace Fragments.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsEmailAlreadyExistsAsync(string email);
        Task CreateAsync(UserDto user);
        Task<AuthenticateResponseDto> LoginAsync(AuthenticateRequestDto model);
        Task<UserDto> GetMeAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task UpdateAsync(UserDto user);
        bool SetChannelValues(ChannelsOfRefference existingChannel, ChannelsOfRefferenceDto channel);
        bool RemoveChannelsOfRefference(UserDto user, ChannelsOfRefference existingChannel);
    }
}
