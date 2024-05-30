namespace Monitoring.DomainExpirationChecker.Interface
{
    public interface IDomainExpiertionStore
    {
        void Save(string requestUri, string expiration);
        string? Find(string requestUri);
    }
}
