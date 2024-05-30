namespace Common.Application.DataTableConfig;

public static class PaggingDataTableExtention
{
    public static void ConfigPaging(ref FiltersFromRequestDataTableBase filtersFromRequest, int recordsTotal)
    {
        if (filtersFromRequest.pageSize == -1)
        {
            filtersFromRequest.pageSize = recordsTotal;
            filtersFromRequest.skip = 0;
        }
    }
}