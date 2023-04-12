namespace Core.Models
{
    public class PaginationResponseModel
    {
        public int PageIndex { get; set; }
        public object Data { get; set; }
        public int PageSize { get; set; } = 0;
        public int TotalItemCount { get; set; } = 0;
        public int TotalPagesCount { get; set; } = 0;
    }
}
