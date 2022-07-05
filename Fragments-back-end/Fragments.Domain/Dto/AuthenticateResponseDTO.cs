using Fragments.Data.Entities;

namespace Fragments.Domain.Dto
{
    public class AuthenticateResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Photo { get; set; } = null!;
        public bool RepresentativeHEI { get; set; }
        public bool RepresentativeAuthority { get; set; }
        public string? Benefits { get; set; }
        public string? Interests { get; set; }
        public virtual ICollection<ChannelsOfRefference>? ChannelsOfRefferences { get; set; }
        public string Token { get; set; } = null!;

        public AuthenticateResponseDTO(User user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Birthday = user.Birthday;
            Photo = user.Photo;
            RepresentativeHEI = user.RepresentativeHEI;
            RepresentativeAuthority = user.RepresentativeAuthority;
            Interests = user.Interests;
            ChannelsOfRefferences = user.ChannelsOfRefferences;
            Token = token;
        }
    }
}
