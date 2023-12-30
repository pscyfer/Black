using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Exceptions
{
    public class CantConvertAddressToHostNameException:BaseApplicationExceptions
    {
        public CantConvertAddressToHostNameException()
        {
            
        }

        public CantConvertAddressToHostNameException(string message)
            :base(message)
        {
            
        }
    }
}
