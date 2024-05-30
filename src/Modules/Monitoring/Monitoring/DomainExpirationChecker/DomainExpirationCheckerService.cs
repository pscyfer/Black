using Common.Application;
using Common.Application.DateUtil;
using Common.Application.Exceptions;
using Microsoft.Extensions.Logging;
using Monitoring.DomainExpirationChecker.Interface;

namespace Monitoring.DomainExpirationChecker
{
    public class DomainExpirationCheckerService : IDomainExpirationCheckerService
    {
        private readonly IDomainExpiertionStore _expiertionStore;
        private readonly ILogger<DomainExpirationCheckerService> _logger;
        private const string DateTimeExpierKey = "DomainExpirationChecker_{0}";
        public DomainExpirationCheckerService(IDomainExpiertionStore expiertionStore, ILogger<DomainExpirationCheckerService> logger)
        {
            _expiertionStore = expiertionStore;
            _logger = logger;
        }
        private string ToLongDate(DateTime date)
        {
            var pc = new PersianDateTime(date);

            return pc.ToLongDateString();
        }
        public OperationResult<DateTime> Check(string checkUri)
        {
            try
            {
                string key = string.Format(DateTimeExpierKey, checkUri);
                var date = _expiertionStore.Find(checkUri);
                if (date == null)
                {
                    var dateResult = DomainExpiration.GetExpirationDate(checkUri);
                    _expiertionStore.Save(checkUri, dateResult.ToString());
                    return OperationResult<DateTime>.Success(dateResult);
                }
                return OperationResult<DateTime>.Success(date.ToDateTime());

            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }
    }
}
