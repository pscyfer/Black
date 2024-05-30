namespace Common.AspNetCore.DataTableConfig
{
    /// <summary>
    /// Represents button edit render for DataTables column
    /// </summary>
    public partial class RenderButtonDetails : IRender
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the RenderButtonEdit class 
        /// </summary>
        public RenderButtonDetails(DataUrl dataUrl, string displayName)
        {
            DisplayName = displayName;
            DataUrl = dataUrl;

        }
        public RenderButtonDetails(DataUrl dataUrl, string displayName,bool isAjax)
        {
            DisplayName = displayName;
            DataUrl = dataUrl;
            IsAjaxEdit = IsAjaxEdit;
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