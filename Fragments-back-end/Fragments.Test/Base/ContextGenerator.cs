using Fragments.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Fragments.Test.Base
{
    internal static class ContextGenerator
    {
        internal static FragmentsContext GetContext()
        {
            var context = new DbContextOptionsBuilder<FragmentsContext>()
            .UseInMemoryDatabase(databaseName: "FragmentsDb")
            .Options;

            return new FragmentsContext(context);
        }
    }
}
