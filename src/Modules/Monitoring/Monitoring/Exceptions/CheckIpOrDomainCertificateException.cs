using Common.Application.Exceptions;

namespace Monitoring.Exceptions
{
    internal class CheckIpOrDomainCertificateException : BaseApplicationExceptions
    {
        public CheckIpOrDomainCertificateException()
        {
            
        }
        public CheckIpOrDomainCertificateException(string message)
            : base(message)
        {

        }
    }
}
