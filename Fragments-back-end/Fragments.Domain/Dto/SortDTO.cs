using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Dto
{
    public class SortDTO
    {
        public bool IsAscending { get; set; }
        public string PropertyName { get; set; } = null!;
    }
}
