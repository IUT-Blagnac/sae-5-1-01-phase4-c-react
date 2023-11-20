using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class EntityContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RoleUser> RoleUsers { get; set; }
    
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
            .HasKey(c => c.id);

        // #-----------#
        // # Role User #
        // #-----------#
        modelBuilder.Entity<RoleUser>()
            .HasKey(ru => ru.id);
        modelBuilder.Entity<RoleUser>()
            .Property(ru => ru.id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .HasOne(u => u.role_user)
            .WithMany(r => r.users)
            .HasForeignKey(u => u.role_id);
    }
}