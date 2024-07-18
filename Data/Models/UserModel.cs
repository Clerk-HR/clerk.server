using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clerk.server.Data.Models;

public class UserModel
{
    [Key]
    public Guid Id { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    [ForeignKey(nameof(UserAccountModel))]
    public Guid AccountId { get; set; }
    public UserAccountModel Account { get; set; } = new();

    [ForeignKey(nameof(WorkspaceModel))]
    public Guid WorkspaceId { get; set; }
    public WorkspaceModel Workspace { get; set; } = new();

}
