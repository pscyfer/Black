namespace Common.AspNetCore.DataTableConfig
{
    /// <summary>
    /// Represents button edit render for DataTables column
    /// </summary>
    public  class RenderButtonEdit : IRender
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the RenderButtonEdit class 
        /// </summary>
        /// <param name="url">URL to the edit action</param>
        public RenderButtonEdit(DataUrl dataUrl, string displayName)
        {
            DisplayName = displayName;
            DataUrl = dataUrl;
        }
        public RenderButtonEdit(DataUrl dataUrl, string displayName, bool isAjaxEdit)
        {
            DisplayName = displayName;
            DataUrl = dataUrl;
            IsAjaxEdit = isAjaxEdit;
        }
        #endregion

        #region Properties
        public string DisplayName { get; set; }
        public DataUrl DataUrl { get; set; }
        /// <summary>
        /// Gets or sets button class name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// عملیات ویرایش  به صورت ajax انجام شود؟
        /// </summary>
        public bool IsAjaxEdit { get; set; }
        #endregion
    }
}