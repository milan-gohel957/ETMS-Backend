namespace ETMS.Domain.DTOs
{
    public class PaginationDTO<T>
    {
        public List<T> Items { get; set; } = [];
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        // Additional helpful fields
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;
        public int? NextPage => HasNextPage ? Page + 1 : (int?)null;
        public int? PreviousPage => HasPreviousPage ? Page - 1 : (int?)null;
    }
}