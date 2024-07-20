using System.ComponentModel.DataAnnotations;

namespace clerk.server.Data.Models;

public class OrganizationModel
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string InviteCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public List<MemberModel> Members { get; set; } = new();
}