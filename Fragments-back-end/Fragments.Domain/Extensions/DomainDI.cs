using Fragments.Data.Context;
using Fragments.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Fragments.Domain.Extensions
{
    public static class DomainDI
    {
        public static void AddDomainDataServices(this IServiceCollection services)
        {
            services.AddDbContext<FragmentsContext>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
