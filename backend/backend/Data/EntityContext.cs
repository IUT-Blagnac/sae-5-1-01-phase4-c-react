using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class EntityContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected readonly IConfiguration _configuration;

    public EntityContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DatabaseConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(c => c.Username);
    }
}