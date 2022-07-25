using Microsoft.EntityFrameworkCore;

namespace Fragments.Domain.Helpers
{
    public class PaginatedList<T> : List<T>
    {

        public PaginatedList(List<T> items, int count, int pageIndex)
        {
            int pageSize = 100; //Add to new Const file (as amount of notifications per page)
            int PageIndex = pageIndex;
            int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            bool HasPreviousPage = PageIndex > 1;
            bool HasNextPage = PageIndex < TotalPages;
            this.AddRange(items);
        }

        //private bool HasPreviousPage => PageIndex > 1;

        //private bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex)
        {
            int pageSize = 100;//Add to new Const file (as amount of notifications per page)
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex);
        }
    }
}
