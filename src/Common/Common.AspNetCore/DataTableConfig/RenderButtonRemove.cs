namespace Common.AspNetCore.DataTableConfig
{
    /// <summary>
    /// Represents button render for DataTables column
    /// </summary>
    public class RenderButtonRemove : IRender
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the RenderButton class 
        /// </summary>
        /// <param name="title">Button title</param>
        public RenderButtonRemove(DataUrl url, string displayName, string className)
        {
            ClassName = className;
            DisplayName = displayName;
            DataUrl = url;
        }

        public RenderButtonRemove(DataUrl url, string displayName)
        {
            DisplayName = displayName;
            DataUrl = url;
        }

        public RenderButtonRemove(DataUrl url, string displayName, bool isAjaxEdit)
        {
            DisplayName = displayName;
            DataUrl = url;
            IsAjaxEdit = isAjaxEdit;
        }

        #endregion

        public string DisplayName { get; set; }
        public DataUrl DataUrl { get; set; }

        #region Properties


        /// <summary>
        /// Gets or sets button class name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// عملیات ویرایش  به صورت ajax انجام شود؟
        /// </summary>
        public bool IsAjaxEdit { get; set; }
        /// <summary>
        /// فیلد که باید مقدار ان چک شود نامش را وارد کنید
        /// </summary>

        public string FieldName { get; set; }
        #endregion

        /// <summary>
        /// Represents button render for DataTables column
        /// </summary>

    }
}