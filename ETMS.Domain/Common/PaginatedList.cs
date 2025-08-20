using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core; // You need to add this NuGet package: System.Linq.Dynamic.Core
using System.Threading.Tasks;

namespace ETMS.Domain.Common;

public class PaginatedList<T>
{
    /// <summary>
    /// The items for the current page.
    /// </summary>
    public List<T> Items { get; }

    /// <summary>
    /// The current page index (1-based).
    /// </summary>
    public int PageIndex { get; }

    /// <summary>
    /// The total number of pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// The total count of items across all pages.
    /// </summary>
    public int TotalCount { get; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage => PageIndex > 1;

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// Creates a paginated list from a source IQueryable asynchronously.
    /// </summary>
    /// <param name="source">The source IQueryable to paginate.</param>
    /// <param name="pageIndex">The 1-based index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="sortOrder">The sort order string (e.g., "Name asc", "Date desc").</param>
    /// <param name="searchPredicate">An optional predicate to filter items.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the PaginatedList<T>.</returns>
    public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source,
        int pageIndex,
        int pageSize,
        string? sortOrder = null,
        System.Linq.Expressions.Expression<Func<T, bool>>? searchPredicate = null)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        // 1. Apply Search Filter
        if (searchPredicate != null)
        {
            source = source.Where(searchPredicate);
        }

        var count = await source.CountAsync();

        // 2. Apply Sorting
        // Using System.Linq.Dynamic.Core for string-based sorting.
        // This is safer and more flexible than building expression trees manually.
        if (!string.IsNullOrWhiteSpace(sortOrder))
        {
            // The library will parse the string and apply OrderBy/ThenBy accordingly.
            // Example: "Name asc, CreatedDate desc"
            source = source.OrderBy(sortOrder);
        }

        // 3. Apply Pagination
        // The Skip() and Take() methods execute the query on the database.
        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}