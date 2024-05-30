namespace Common.AspNetCore.DataTableConfig
{
    /// <summary>
    /// Represents custom render for DataTables column
    /// </summary>
    public partial class RenderCustom : IRender
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the RenderCustom class 
        /// </summary>
        public RenderCustom()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets custom render function name(js)
        /// See also https://datatables.net/reference/option/columns.render
        /// </summary>
        public string FunctionName { get; set; }

        public string Template { get; set; }

        #endregion
    }
}