using System.ComponentModel.DataAnnotations;

namespace clerk.server.Data.Models;

public class UserAccountModel
{
    [Key]
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public OnBoarding OnBoarding { get; set; } = OnBoarding.CreateAccount;
}

public enum OnBoarding
{
    CreateAccount,
    UserDetails,
    Organization,
    Complete
}