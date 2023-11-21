using backend.Data.Models;
using backend.FormModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class EntityContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RoleUser> RoleUsers { get; set; }
    public DbSet<Team> Equipes { get; set; }
    
    protected readonly IConfiguration _configuration;
    private readonly PasswordHasher<User> _passwordHasher;

    public EntityContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
    }

    public void InitializeDefaultData()
    {
        //DEFAULT ROLES

        var defaultRoles = new List<RoleUser>
        {
            new() { name = "Student" },
            new() { name = "Teacher" },
            new() { name = "Admin" },
        };

        foreach(var role in defaultRoles)
        {
            if (!RoleUsers.Where(c => c.name == role.name).Any())
            {
                RoleUsers.Add(role);
            }
        }

        SaveChanges();

        //DEFAULT ADMIN USER

        if (!Users.Where(c => c.email == "admin@superadmin.com").Any())
        {
            var defaultAdmin = new User
            {
                email = "admin@superadmin.com",
                role_user = RoleUsers.Where(c => c.name == "Admin").FirstOrDefault(),
                first_name = "Admin",
                last_name = "SuperAdmin",
            };

            defaultAdmin.password = _passwordHasher.HashPassword(defaultAdmin, "isfqzA8@&Ne@y9Ls@9CK");
            Users.Add(defaultAdmin);
        }

        SaveChanges();
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

        // #------#
        // # User #
        // #------#
        modelBuilder.Entity<User>()
            .HasKey(u => u.id);

        modelBuilder.Entity<User>()
            .HasOne(u => u.role_user)
            .WithMany(r => r.users)
            .HasForeignKey(u => u.role_id);

        modelBuilder.Entity<User>()
            .HasOne(u => u.group)
            .WithMany(g => g.users)
            .HasForeignKey(u => u.id_groupe);

        // #-------#
        // # Group #
        // #-------#
        modelBuilder.Entity<Group>()
            .HasKey(u => u.id);

        modelBuilder.Entity<Group>()
            .HasOne(g => g.group_parent)
            .WithMany(g => g.groups_childs)
            .HasForeignKey(g => g.id_groupe_parent);

        modelBuilder.Entity<Group>()
            .HasOne(g => g.prom)
            .WithMany(p => p.groups)
            .HasForeignKey(g => g.id_prom);

        // #------#
        // # Prom #
        // #------#
        modelBuilder.Entity<Prom>()
            .HasKey(u => u.id);

        // #-----#
        // # Sae #
        // #-----#
        modelBuilder.Entity<Sae>()
            .HasKey(u => u.id);

        modelBuilder.Entity<Sae>()
            .HasOne(s => s.prom)
            .WithMany(p => p.saes)
            .HasForeignKey(s => s.id_prom);

        // #------#
        // # Team #
        // #------#
        modelBuilder.Entity<Team>()
            .HasKey(c => c.id);
    }
}