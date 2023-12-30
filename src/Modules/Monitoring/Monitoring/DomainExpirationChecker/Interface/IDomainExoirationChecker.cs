using Common.Application;

namespace Monitoring.DomainExpirationChecker.Interface
{
    public interface IDomainExpirationCheckerService
    {
        OperationResult<DateTime> Check(string checkUri);
    }
}
