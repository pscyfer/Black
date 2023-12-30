namespace Monitoring.Abstractions.ViewModels;
public class GetMonitorNamebyIdViewModel
{
    public GetMonitorNamebyIdViewModel()
    {
        
    }
    public string Name { get; set; }
    public long Id { get; set; }
    public bool IsPausedValue { get; set; }
    public GetMonitorNamebyIdViewModel(string name, long id = 0, bool isPausedValue = false)
    {
        Name = name;
        Id = id;
        IsPausedValue = isPausedValue;
    }
}
