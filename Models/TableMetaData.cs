namespace RPGClient.Models;

public class TableMetaData
{
    public string? SortingPropName { get; set; }
    public bool Ascending { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}