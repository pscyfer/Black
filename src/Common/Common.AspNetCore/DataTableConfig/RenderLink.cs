namespace Common.AspNetCore.DataTableConfig
{
    /// <summary>
    /// Represents link render for DataTables column
    /// </summary>
    public partial class RenderLink : IRender
    {
        #region Ctor
        public RenderLink(DataUrl url, string title)
        {

            Url = url;
            Title = title;
            Target = "_Blank";
        }
        /// <summary>
        /// Initializes a new instance of the RenderButton class 
        /// </summary>
        /// <param name="url">URL</param>
        public RenderLink(DataUrl url, string title, string target= "_Blank", string classNames = "btn")
        {
            Url = url;
            Title = title;
            Target = target;
            ClassNames = classNames;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Url
        /// </summary>

        /// <summary>
        /// Gets or sets link title 
        /// </summary>
        public string Title { get; set; }
        public DataUrl Url { get; set; }
        public string Target { get; set; }
        public string ClassNames { get; set; }
        #endregion
    }
}