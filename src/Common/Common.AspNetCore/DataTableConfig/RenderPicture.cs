namespace Common.AspNetCore.DataTableConfig;
/// <summary>
/// Represents picture render for DataTables column
/// </summary>
public class RenderPicture : IRender
{
    #region Ctor
    public RenderPicture()
    {
        BaseUrl = "~/modules/identity/default.png";
    }
    public RenderPicture(string baseUrl, string width, string height)
    {
        if (!string.IsNullOrWhiteSpace(baseUrl)) BaseUrl = baseUrl;
        Width = width;
        Height = height;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets picture URL prefix
    /// </summary>
    public string BaseUrl { get; set; }
    public string Width { get; set; }
    public string Height { get; set; }
    /// <summary>
    /// Gets or sets picture source
    /// </summary>

    #endregion
}
