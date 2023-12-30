using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Common.AspNetCore.DataTableConfig
{
    public class DataUrl
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of the DataUrl class 
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        private readonly IUrlHelper _urlHelper;
        public DataUrl(string actionName, string controllerName, RouteValueDictionary routeValues, IUrlHelper urlHelper)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            RouteValues = routeValues;
            _urlHelper = urlHelper;
        }

        /// <summary>
        /// Initializes a new instance of the DataUrl class 
        /// </summary>
        /// <param name="url">URL</param>
        public DataUrl(string url, IUrlHelper urlHelper)
        {
            Url = url;
            _urlHelper = urlHelper;
        }

        /// <summary>
        /// Initializes a new instance of the DataUrl class 
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="dataId">Name of the column whose value is to be used as identifier in URL</param>
        public DataUrl(string url, string dataId, IUrlHelper urlHelper)
        {
            Url = url;
            DataId = dataId;
            _urlHelper = urlHelper;
        }

        /// <summary>
        /// Initializes a new instance of the DataUrl class 
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="trimEnd">Parameter indicating that you need to delete all occurrences of the character "/" at the end of the line</param>
        public DataUrl(string url, bool trimEnd, IUrlHelper urlHelper)
        {
            Url = url;
            TrimEnd = trimEnd;
            _urlHelper = urlHelper;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the route values.
        /// </summary>
        public RouteValueDictionary RouteValues { get; set; }

        /// <summary>
        /// Gets or sets data Id
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// Gets or sets parameter indicating that you need to delete all occurrences of the character "/" at the end of the line
        /// </summary>
        public bool TrimEnd { get; set; }

        public IRender Render { get; set; }

        #endregion

        #region Methdos
        public string GetUrl()
        {
            return !string.IsNullOrEmpty(ActionName) && !string.IsNullOrEmpty(ControllerName)
                ? _urlHelper.Action(ActionName,ControllerName,RouteValues)
                : !string.IsNullOrEmpty(Url)
                    ? $"{(Url.StartsWith("~/", StringComparison.Ordinal) ? _urlHelper.Content(Url) : Url).TrimEnd('/')}" + (!TrimEnd ? "/" : "")
                    : string.Empty;
        }

        #endregion

    }
}
