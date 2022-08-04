using Microsoft.EntityFrameworkCore;
using Fragments.Data.Constants;

namespace Fragments.Domain.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int TotalPages { get; private set; }
        public PaginatedList(List<T> items, int count, int pageIndex)
        {
            int pageSize = Constants.NotificationsOnOnePage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex)
        {
            int pageSize = Constants.NotificationsOnOnePage;
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex);
        }
    }
}
