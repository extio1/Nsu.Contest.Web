namespace Nsu.Contest.Web.HRManager.Model.Data;

using Nsu.Contest.Web.Common.Entity; 
using Nsu.Contest.Web.HRManager.Model.Data.EntityConfiguration;

using Microsoft.EntityFrameworkCore;

public class HRManagerDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Junior> Juniors { get; set; }
    public DbSet<Teamlead> Teamleads { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }

    public HRManagerDbContext(DbContextOptions<HRManagerDbContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRManagerDbContext).Assembly);
        // modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
}
