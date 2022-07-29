namespace Fragments.Data.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public virtual ICollection<UsersRole> UsersRole { get; set; } = null!;
    }
}