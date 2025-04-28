using BlazarTech.QueryableValues;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreTests;

public class MyDbContext : DbContext
{
    public DbSet<EntityWithCompositeKey> EntityWithCompositeKeys { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test;Trusted_Connection=True;",
            opt =>
            {
                opt.UseQueryableValues();
            })
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EntityWithCompositeKey>().HasKey(e => new { e.Id1, e.Id2 });
    }
}