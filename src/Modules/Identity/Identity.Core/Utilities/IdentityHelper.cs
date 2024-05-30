using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Core.Utilities;

public static class IdentityHelper
{
    public static string GetErrors(this IdentityResult errors)
    {
        if (errors.Errors is null) throw new ArgumentIsNullOrEmptyException(nameof(errors));
        return string.Join(", ", errors.Errors.Select(error => error.Description));
    }
}