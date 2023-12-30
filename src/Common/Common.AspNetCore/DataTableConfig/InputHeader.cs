using Microsoft.AspNetCore.Mvc.Rendering;

namespace Common.AspNetCore.DataTableConfig
{
    /// <summary>
    /// اینپوت ها برای فیلتر کردن دیتا بر اساس این 
    /// </summary>
    public class InputHeader
    {
        public InputHeader()
        {

        }

        public string Id { get; set; }
        public string Title { get; set; }
        public InputType InputType { get; set; }
        public string PlaceHolder { get; set; }
        public string ClassNames { get; set; }
        public string InputHelperText { get; set; }

        public string ParameterName { get; set; }
        public string TempDataKey { get; set; }
        public List<SelectListItem>? SelectListItems { get; set; }
    }
}
