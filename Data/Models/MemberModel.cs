using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clerk.server.Data.Models;

public class MemberModel
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(UserModel))]
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = new();

    public List<Role> Roles { get; set; } = [Role.Employee];

    [ForeignKey(nameof(OrganizationModel))]
    public Guid OrganizationId { get; set; }
    public OrganizationModel Organization { get; set; } = new();
    public DateTime JoinedOn { get; set; } = DateTime.UtcNow;
}

public enum Role
{
    Employee = 1,
    Manager = 2
}
