namespace Common.AspNetCore.Autorizetion.DynamicPermissions;

public class ActionViewModel
{
    public IList<Attribute> ActionAttributes { get; set; }
    public string? ActionDisplayName { get; set; }
    public string ActionId => $"{ControllerId}:{ActionName}";
    public string ActionName { get; set; }
    public string ControllerId { get; set; }
    public bool IsSecuredAction { get; set; }
}