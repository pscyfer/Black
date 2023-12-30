namespace Identity.Core.Dto.User;

public class GetUserConfirmtionInfoDto
{
    public string UserName { get; set; }
    public bool IsActive { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public int AccessFailedCount { get; set; }
}