using Fragments.Data.Entities;
using Fragments.Domain.Helpers;
using Microsoft.Extensions.Configuration;

namespace Fragments.Domain.Extensions
{
    public class ExtensionsWrapper : IExtensionsWrapper
    {
        private readonly IConfiguration _configuration;
        public ExtensionsWrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetJwtToken(User user)
        {
            return _configuration.GenerateJwtToken(user);
        }
    }
}
