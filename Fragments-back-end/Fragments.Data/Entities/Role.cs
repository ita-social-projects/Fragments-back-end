namespace Fragments.Data.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}