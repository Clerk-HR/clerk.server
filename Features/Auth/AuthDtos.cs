using System.Security.Principal;
using clerk.server.Data.Models;

namespace clerk.server.Features.Auth;

public record CreateAccountDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public record UserAccountDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public OnBoarding OnBoarding { get; set; }
}

public record CreateAccountResponseDto
{
    public UserAccountDto Account { get; set; } = new();
    public string Token { get; set; } = string.Empty;
}