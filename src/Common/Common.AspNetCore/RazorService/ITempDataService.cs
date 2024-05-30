using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Common.AspNetCore.RazorService
{

    public interface ITempDataService
    {
        Task<List<SelectListItem>> GetSelectListItems(string tempDataKey);
    }

    public class TempDataService : ITempDataService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public TempDataService(IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }
        public async Task<List<SelectListItem>> GetSelectListItems(string tempDataKey)
        {

            try
            {
                var SelectListItems = new List<SelectListItem>();
                await Task.Run(() =>
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);

                    var result = tempData[tempDataKey];

                    var convertToSelectListType = result as List<SelectListItem>;
                    if (convertToSelectListType != null)
                    {
                        SelectListItems = convertToSelectListType.ToList();
                    }

                });

                return SelectListItems;
            }
            catch (Exception)
            {
                throw new Exception($"tempDataKey not set value ,{tempDataKey}");
            }


        }
    }
}
