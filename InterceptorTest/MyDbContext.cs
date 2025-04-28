using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InterceptorTest;

public class MyDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=InterceptorTest;Trusted_Connection=True;")
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
        
        optionsBuilder.AddInterceptors(new CreatedAtModifiedAtInterceptor());
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyEntity>().ToTable("MyEntity");
        base.OnModelCreating(modelBuilder);
    }
}