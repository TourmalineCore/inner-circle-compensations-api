using Core;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

//Use next command in Package Manager Console to update Dev env DB
//PM> $env:ASPNETCORE_ENVIRONMENT = 'Debug'; Update-Database
public class CompensationsDbContext : DbContext
{
    public virtual DbSet<Compensation> Compensations { get; set; }

    public CompensationsDbContext(DbContextOptions<CompensationsDbContext> options) : base(options)
    {

    }

    // for the sake of Moq library for Tests of DbSet
    public CompensationsDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
