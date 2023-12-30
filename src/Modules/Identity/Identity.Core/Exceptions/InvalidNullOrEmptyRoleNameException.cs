using Common.Application.Exceptions;

namespace Identity.Core.Exceptions;

public class InvalidNullOrEmptyRoleNameException:BaseApplicationExceptions
{
  
    public InvalidNullOrEmptyRoleNameException(string message):base(message)
    {
        
    }
}