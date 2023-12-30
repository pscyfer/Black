namespace Identity.Core.Dto.Claim;

public class RoleClaimDto
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue{ get; set; }
}
public class AddRoleClaimDto
{

    public Guid RoleId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}