namespace Common.Application.DataTableConfig;
public static class DataTableExtension
{

    public static PaginationDataTableResult<T> ToDataTableJs<T>(this IQueryable<T> source, FiltersFromRequestDataTableBase filtersFromRequest)
    {
        int recordsTotal = source.Count();
        CofingPaging(ref filtersFromRequest, recordsTotal);
        var result = new PaginationDataTableResult<T>()
        {
            draw = filtersFromRequest.draw,
            recordsFiltered = recordsTotal,
            recordsTotal = recordsTotal,
            data = source.OrderByIndex(filtersFromRequest).Skip(filtersFromRequest.skip).Take(filtersFromRequest.pageSize).ToList()
        };

        return result;
    }

    private static void CofingPaging(ref FiltersFromRequestDataTableBase filtersFromRequest, int recordsTotal)
    {
        if (filtersFromRequest.pageSize == -1)
        {
            filtersFromRequest.pageSize = recordsTotal;
            filtersFromRequest.skip = 0;
        }
    }
    public static IEnumerable<TSource> WhereSearchValue<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        return source.Where(predicate);
    }
    public static bool ContainsSearchValue(this string source, string toCheck)
    {
        return source != null && toCheck != null && source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
    }
    private static IEnumerable<T> OrderByIndex<T>(this IEnumerable<T> source, FiltersFromRequestDataTableBase filtersFromRequest)
    {
        var props = typeof(T).GetProperties();
        string propertyName = "";
        for (int i = 0; i < props.Length; i++)
        {
            if (i.ToString() == filtersFromRequest.sortColumnIndex)
                propertyName = props[i].Name;
        }

        System.Reflection.PropertyInfo propByName = typeof(T).GetProperty(propertyName);
        if (propByName is not null)
        {
            if (filtersFromRequest.sortColumnDirection == "desc")
                source = source.OrderByDescending(x => propByName.GetValue(x, null));
            else
                source = source.OrderBy(x => propByName.GetValue(x, null));
        }

        return source;
    }
}