
using clerk.server.Data.Models;

namespace clerk.server.Features.Auth;

public record RegisterDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public record LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}


public record AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
}
