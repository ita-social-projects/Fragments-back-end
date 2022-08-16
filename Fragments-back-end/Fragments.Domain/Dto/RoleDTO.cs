using Fragments.Data.Entities;

namespace Fragments.Domain.Dto
{
    public class RoleDto
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public virtual ICollection<UsersRole> UsersRole { get; set; } = null!;
    }
}
