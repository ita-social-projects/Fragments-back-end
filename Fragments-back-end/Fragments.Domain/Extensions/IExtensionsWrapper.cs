using Fragments.Data.Entities;

namespace Fragments.Domain.Extensions
{
    public interface IExtensionsWrapper
    {
        string GetJwtToken(User user);
    }
}
