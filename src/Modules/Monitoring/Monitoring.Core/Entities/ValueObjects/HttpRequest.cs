using Common.Domain;
using System.Net;

namespace Monitoring.Core.Entities.ValueObjects
{

    public class HttpRequest : ValueObject
    {
        public HttpRequest()
        {
            Method = Enums.HttpMethod.Get;
            StatusCode = HttpStatusCode.OK;
        }
        public HttpStatusCode StatusCode { get; set; }
        public Enums.HttpMethod Method { get; set; }
        public bool IsSslVerification { get; set; }
        public bool IsDomainCheck { get; set; }
        public DateTime DomainExpierDate { get; set; }
        public void Add(HttpStatusCode code, Enums.HttpMethod method, bool isSslVerification, bool isDomainCheck)
        {

            StatusCode = code;
            Method = method;
            IsSslVerification = isSslVerification;
            IsDomainCheck = isDomainCheck;
        }

        public void AddDomingExpierDate(DateTime date)
        {
            DomainExpierDate = date;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        public void Edit(bool commandIsDomainCheck, bool commandIsSslVerification)
        {
            IsDomainCheck = commandIsDomainCheck;
            IsSslVerification = commandIsSslVerification;
        }
    }


}
