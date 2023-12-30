namespace Common.AspNetCore.DataTableConfig;

public record RenderButtonCustome : IRender
{
    #region Ctor

    /// <summary>
    /// Initializes a new instance of the RenderButton class 
    /// </summary>
    /// <param name="title">Button title</param>
    public RenderButtonCustome(DataUrl url, string displayName, string className, bool isAjaxEdit = false)
    {
        ClassName = className;
        DisplayName = displayName;
        DataUrl = url;
        IsAjaxEdit = isAjaxEdit;
    }
    public RenderButtonCustome(DataUrl url, string displayName)
    {
        DisplayName = displayName;
        DataUrl = url;
    }
    public RenderButtonCustome(DataUrl url, string displayName, bool isAjax)
    {
        DisplayName = displayName;
        DataUrl = url;
        IsAjaxEdit = isAjax;
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
    /// قرار است با سمت دیتابیس کاری انجام دهد.
    /// </summary>
    public bool WorkDataOperation { get; set; }
    /// <summary>
    /// فیلد که باید مقدار ان چک شود نامش را وارد کنید
    /// </summary>

    public string FieldName { get; set; }

    /// <summary>
    /// نوع یا تایپ خروجی چیه که براساس آن تایپ شرط گذاشته شود
    /// </summary>
    public string TypeOfOutput { get; set; }
    /// <summary>
    /// اگر مقدار وارد شده برار با true بود
    /// </summary>
    public Dictionary<object,string> IfEqualResultContext { get; set; }
    public Dictionary<object,string> IfNotEqualResultContext { get; set; }
    #endregion
}