using Fragments.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Dto
{
    public class AdminDTO
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
