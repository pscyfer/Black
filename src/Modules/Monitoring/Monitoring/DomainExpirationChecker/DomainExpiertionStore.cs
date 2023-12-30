using Microsoft.Extensions.Caching.Memory;
using Monitoring.DomainExpirationChecker.Interface;

namespace Monitoring.DomainExpirationChecker
{
    public class DomainExpiertionStore : IDomainExpiertionStore
    {
        private readonly IMemoryCache _cache;

        public DomainExpiertionStore(IMemoryCache cache)
        {
            _cache = cache;
        }

        private const int EXPIRY_MINUTES = 60;
        public string? Find(string requestUri)
        {
            return _cache.Get<string>(requestUri);
        }

        public void Save(string requestUri, string expiration)
        {
            _cache.Set(requestUri, expiration, TimeSpan.FromMinutes(EXPIRY_MINUTES));
        }
    }
}
