using clerk.server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace clerk.server.Data.Repository;

public class RepositoryContext : DbContext
{

    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<OrganizationModel> Organizations { get; set; }
    public DbSet<MemberModel> Members { get; set; }
}