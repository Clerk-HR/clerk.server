using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clerk.server.Data.Models;

public class WorkspaceModel
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = "default";
}