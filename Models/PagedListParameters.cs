namespace RPGClient.Models;

public class PagedListParameters
{
    public string? SortingPropName { get; set; }
    public string? SearchText { get; set; }
    public bool Ascending { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}