
namespace ProjectManagementSystem.Application.Common;

public class PagedResult<T> where T : class
{
    public PagedResult(IEnumerable<T> items, int totalItemsCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalItemsCount;
        TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = Math.Min(ItemsFrom + pageSize - 1, totalItemsCount);
    }
    
    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
