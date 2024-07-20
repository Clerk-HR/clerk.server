using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clerk.server.Data.Models;

public class UserModel
{
    [Key]
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public OnBoarding OnBoarding { get; set; } = OnBoarding.CreateAccount;
    public DateTime CreatedOn { get; } = DateTime.UtcNow;

    [ForeignKey(nameof(MemberModel))]
    public Guid? MemberId { get; set; }
    public MemberModel? Profile { get; set; }

}

public enum OnBoarding
{
    CreateAccount,
    UserDetails,
    JoinCreate,
    Complete
}
