using Fragments.Domain.Dto;

namespace Fragments.Domain.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IReadOnlyList<UserDTO>> GetUserWithSearchAsync(string text);
    }
}
