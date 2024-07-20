using clerk.server.Features.Member;

namespace clerk.server.Features.Organization;

public record OrganizationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string InviteCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? logoUrl { get; set; }
    public List<MemberDto> Members { get; set; } = new();

}

public record CreateOrganizationDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? logoUrl { get; set; }
}


public record JoinDto
{
    public string Code { get; set; } = string.Empty;
}