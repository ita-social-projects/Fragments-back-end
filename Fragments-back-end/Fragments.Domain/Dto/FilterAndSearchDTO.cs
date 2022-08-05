using Fragments.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Dto
{
    public class FilterAndSearchDTO
    {
        public List<string> Roles { get; set; } = null!;
        public bool RepresentativeHEI { get; set; }
        public bool RepresentativeAuthority { get; set; }
        public string SearchText { get; set; } = null!; 

    }
}
