namespace Common.AspNetCore.DataTableConfig;
public class ButtonHeader
{
    public ButtonHeader()
    {

    }
    public ButtonHeader(string textHtml)
    {
        TextHtml = textHtml;
    }
    public ButtonHeader(string textHtml, string className)
    {
        TextHtml = textHtml;
        ClassName = className;
    }
    public ButtonHeader(string textHtml, string className, DataUrl url, bool isAjaxRender)
    {
        TextHtml = textHtml;
        ClassName = className;
        DataUrl = url;
        IsAjaxRender = isAjaxRender;
    }
    public string Name { get; set; }
    public string TextHtml { get; set; }
    public string ClassName { get; set; }
    public bool IsAjaxRender { get; set; }
    public DataUrl DataUrl { get; set; }
}


