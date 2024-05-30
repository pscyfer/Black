using System.Net;

namespace Monitoring.Abstractions.DTOs.ResponseTime;
public class GetResponseTimeDto
{
    public long ResponseTime { get; set; }
    public bool IsSuccessResponse { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string? Reason { get; set; }


}
