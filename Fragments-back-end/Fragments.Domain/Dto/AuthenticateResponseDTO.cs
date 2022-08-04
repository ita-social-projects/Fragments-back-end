using Fragments.Data.Entities;

namespace Fragments.Domain.Dto
{
    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } 
        public string Token { get; set; } 

        public AuthenticateResponseDto(User user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Token = token;
        }
    }
}
