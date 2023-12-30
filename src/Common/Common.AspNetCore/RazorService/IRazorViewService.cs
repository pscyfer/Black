namespace Common.AspNetCore.RazorService
{
    public interface IRazorViewService
    {
        Task<string> RenderViewToString(string viewName, object model);
    }
}
