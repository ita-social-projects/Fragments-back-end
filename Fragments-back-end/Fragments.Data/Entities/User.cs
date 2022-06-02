
namespace Fragments.Data.Entities
{
    public class User
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
        public ICollection<ChannelsOfRefference>? ChannelsOfRefferences { get; set; }
    }
}
