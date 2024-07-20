
namespace clerk.server.Features.Member;

using clerk.server.Data.Models;
using clerk.server.Features.Organization;
using clerk.server.Features.User;

public record MemberDto
{
    public Guid Id { get; set; }
    public UserDto User { get; set; } = new();
    public List<Role> Roles { get; set; } = new();
    public OrganizationDto? Organization { get; set; } 

}