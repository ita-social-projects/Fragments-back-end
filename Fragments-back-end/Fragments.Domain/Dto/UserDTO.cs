using Fragments.Domain.Services;
using Fragments.Domain.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Fragments.Domain.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Photo { get; set; } = null!;
    }
}
