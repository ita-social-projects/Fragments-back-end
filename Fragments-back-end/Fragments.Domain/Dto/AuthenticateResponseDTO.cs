using Fragments.Data.Entities;

namespace Fragments.Domain.Dto
{
    public class AuthenticateResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;

        public AuthenticateResponseDTO(User user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Token = token;
        }
    }
}
