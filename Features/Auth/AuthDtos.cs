
namespace clerk.server.Features.Auth;

public record RegisterDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public record AuthResponseDto
{
    public UserDto User { get; set; } = new();
    public string Token { get; set; } = string.Empty;
}