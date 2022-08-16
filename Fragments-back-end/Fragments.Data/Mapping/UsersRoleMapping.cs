using Fragments.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Data.Mapping
{
    public static class UsersRoleMapping 
    {
        public static void Map(EntityTypeBuilder<UsersRole> entity)
        {
            entity.HasKey(ur => new {ur.UserId, ur.RoleId});
        }
    }
}
