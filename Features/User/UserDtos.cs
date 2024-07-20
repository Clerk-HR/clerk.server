namespace clerk.server.Features.User;

using clerk.server.Data.Models;
using clerk.server.Features.Member;

public record UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? Fullname { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public OnBoarding OnBoarding { get; set; }
    public DateTime CreatedOn { get; set; }
    public MemberDto? Profile { get; set; }
}


public record UserDetailsDto
{
    public IFormFile? Avatar { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
