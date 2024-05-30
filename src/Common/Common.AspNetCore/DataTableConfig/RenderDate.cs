namespace Common.AspNetCore.DataTableConfig
{
    /// <summary>
    /// Represents date render for DataTables column
    /// </summary>
    public partial class RenderDate : IRender
    {
        #region Constants

        /// <summary>
        /// Default date format
        /// </summary>
        private string DEFAULT_DATE_FORMAT = "YYYT/MM/DD HH:mm:ss";

        #endregion

        #region Ctor

        public RenderDate()
        {
            //set default values
            Format = DEFAULT_DATE_FORMAT;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets format date (moment.js)
        /// See also "http://momentjs.com/"
        /// </summary>
        public string Format { get; set; }

        #endregion
    }
}