using Core;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

//Use next command in Package Manager Console to update Dev env DB
//PM> $env:ASPNETCORE_ENVIRONMENT = 'Debug'; Update-Database
public class CompensationsDbContext : DbContext
{
    public DbSet<Compensation> Compensations { get; set; }

    public CompensationsDbContext(DbContextOptions<CompensationsDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
