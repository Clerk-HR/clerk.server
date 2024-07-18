
using clerk.server.Data.Models;

public record UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public OnBoarding OnBoarding { get; set; }
}