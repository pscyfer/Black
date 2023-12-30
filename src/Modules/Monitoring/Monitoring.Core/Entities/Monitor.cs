using Monitoring.Core.Entities.ValueObjects;

namespace Monitoring.Core.Entities;

/// <summary>
/// جدول مانیتور ها
/// </summary>
public sealed class Monitor
{

    #region ctor

    public Monitor()
    {
        Http = new HttpRequest();
        Events = new List<Event>();
        ResponsTimes = new List<ResponsTimeMonitor>();
        Incidents = new List<Incident>();
    }

    public Monitor(long id, string ip, string name, int interval, int timeout, bool isPause, Guid userId)
    {
        Id = id;
        Ip = ip;
        Name = name;
        Interval = interval;
        Timeout = timeout;
        IsPause = isPause;
        UserId = userId;
    }

    #endregion

    #region Props
    public long Id { get; set; }
    public string Ip { get; set; }
    public string Name { get; set; }
    public int Interval { get; set; }
    public int Timeout { get; set; }

    public bool IsPause { get; set; }
    public DateTime LastChecked { get; set; }
    public DateTime UpTimeFor { get; set; }

    public HttpRequest Http { get; set; }
    /// <summary>
    /// شناسه کاربر
    /// </summary>
    public Guid UserId { get; set; }

    public string OwnerFullName { get; set; }

    #endregion


    #region BaseMethods

    public static Monitor Intance()
    {
        return new Monitor();
    }

    public void CreateDefaultValue(Guid userId, string ownerFullName, string ip, string name, int interval, int timeout)
    {
        Ip = ip;
        Name = name;
        Interval = interval;
        Timeout = timeout;
        UserId = userId;
        OwnerFullName = ownerFullName;
    }
    #endregion

    #region HttpMethods
    public void SetDominExpierDate(DateTime date)
    {
        Http.AddDomingExpierDate(date);
    }
    public void CreateHttpRequest(HttpRequest httpRequest)
    {
        Http.Add(httpRequest.StatusCode, httpRequest.Method, httpRequest.IsSslVerification, httpRequest.IsDomainCheck);
    }
    public void EditHttpRequest(string fullName, string commandName,
        string commandIp, int commandInterval, int commandTimeout,
        bool commandIsSslVerification, bool commandIsDomainCheck, bool isPause)
    {
        OwnerFullName = fullName;
        Name = commandName;
        Ip = commandIp;
        Interval = commandInterval;
        Timeout = commandTimeout;
        IsPause = isPause;
        Http.Edit(commandIsDomainCheck, commandIsSslVerification);
    }
    #endregion
    #region Relation

    public ICollection<Event> Events { get; set; }
    public ICollection<Incident> Incidents { get; set; }
    public ICollection<ResponsTimeMonitor> ResponsTimes { get; set; }

    #endregion


}


