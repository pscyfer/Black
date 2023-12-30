using Common.Application.DataTableConfig;

namespace Administrator.Infrastructure.DataTableHelper
{
    public static class DataTableExtension
    {
       
        public static void GetDataFromRequest(this HttpRequest request, out FiltersFromRequestDataTableBase filtersFromRequest)
        {
            filtersFromRequest = new FiltersFromRequestDataTableBase
            {
                draw = request.Form.TryGetValue("draw", out var drawValue) ? drawValue.FirstOrDefault() : null,
                start = request.Form.TryGetValue("start", out var startValue) ? startValue.FirstOrDefault() : null,
                length = request.Form.TryGetValue("length", out var lengthValue) ? lengthValue.FirstOrDefault() : null
            };

            var orderColumnIndex = request.Form["order[0][column]"].FirstOrDefault();

            filtersFromRequest.sortColumn = request.Form.TryGetValue($"columns[{orderColumnIndex}][name]", out var sortColumnValue)
                ? sortColumnValue.FirstOrDefault()
                : null;

            filtersFromRequest.sortColumnDirection = request.Form.TryGetValue("order[0][dir]", out var sortDirValue)
                ? sortDirValue.FirstOrDefault()
                : null;

            filtersFromRequest.searchValue = request.Form.TryGetValue("search[value]", out var searchValue)
                ? searchValue.FirstOrDefault()
                : null;

            filtersFromRequest.pageSize = int.TryParse(filtersFromRequest.length, out var pageSizeValue) ? pageSizeValue : 0;
            filtersFromRequest.skip = int.TryParse(filtersFromRequest.start, out var skipValue) ? skipValue : 0;
            filtersFromRequest.sortColumnIndex = orderColumnIndex;

            filtersFromRequest.searchValue = filtersFromRequest.searchValue?.ToLower();
        }
    }
}
