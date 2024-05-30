using Microsoft.AspNetCore.Authorization;

namespace Common.AspNetCore.Autorizetion;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public sealed class BaseAuthorizeAttribute : AuthorizeAttribute
{
    public BaseAuthorizeAttribute(params string[] roles)
        : base()
    {
        Roles = $"{string.Join(",", roles)}";
    }
}
