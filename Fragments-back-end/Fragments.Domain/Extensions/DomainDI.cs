using Fragments.Data.Context;
using Fragments.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Fragments.Domain.Extensions
{
    public static class DomainDI
    {
        public static void AddDomainDataServices(this IServiceCollection services)
        {
            services.AddDbContext<IFragmentsContext, FragmentsContext>();
        }
        public static void AddDI(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
