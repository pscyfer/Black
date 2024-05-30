namespace Common.Application;

public static class UrlGenerator
{
    private const string RootPath = @"https://localhost:7020";
    public static string GenerateStaticUrl(this string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return "";
        return RootPath + url;
    }
}