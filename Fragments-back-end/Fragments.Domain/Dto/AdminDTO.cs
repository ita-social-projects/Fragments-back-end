using Fragments.Data.Entities;

namespace Fragments.Domain.Dto
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public bool RepresentativeHEI { get; set; }
        public bool RepresentativeAuthority { get; set; }
        public virtual ICollection<UsersRole> UsersRole { get; set; } = null!;
    }
}
