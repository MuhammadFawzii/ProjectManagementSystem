
namespace ProjectManagementSystem.Application.Common;

public class PagedResult<T> where T : class
{
    public PagedResult(IEnumerable<T> Items, int TotalItemsCount, int pageSize,int PageNumber)
    {
        this.Items = Items;
        this.TotalItemsCount = TotalItemsCount;
        this.TotalPages = (int)Math.Ceiling(TotalItemsCount / (double)pageSize);
        this.ItemsFrom = pageSize * (PageNumber - 1) + 1;
        this.ItemsTo = this.ItemsFrom + pageSize - 1;

    }
    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom {  get; set; }
    public int ItemsTo { get; set; }

}
