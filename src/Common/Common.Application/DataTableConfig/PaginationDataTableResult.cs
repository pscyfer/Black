namespace Common.Application.DataTableConfig;
public class PaginationDataTableResult<T>
{
    public string draw { get; set; }
    public int recordsFiltered { get; set; }
    public int recordsTotal { get; set; }
    public List<T> data { get; set; }
}

