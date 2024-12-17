namespace Nsu.Contest.Web.HRDirector.Model.Data;

using Nsu.Contest.Web.Common.Entity; 

using Microsoft.EntityFrameworkCore;

public class HRDirectorDbContext : DbContext
{
    public DbSet<Contest> Contests { get; set; }

    public HRDirectorDbContext(DbContextOptions<HRDirectorDbContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDirectorDbContext).Assembly);
    }
}
