﻿using backend.Data.Models;
using backend.FormModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class EntityContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RoleUser> RoleUsers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<UserTeam> UserTeams { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    
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

        modelBuilder.Entity<User>()
            .HasOne(u => u.role_user)
            .WithMany(r => r.users)
            .HasForeignKey(u => u.role_id);

        modelBuilder.Entity<Team>()
            .HasKey(c => c.id);

        modelBuilder.Entity<UserTeam>()
            .HasKey(ut => new { ut.user_id, ut.team_id });
            
        modelBuilder.Entity<UserTeam>()
            .HasOne(ut => ut.user)
            .WithMany(u => u.user_team)
            .HasForeignKey(ut => ut.user_id);

        modelBuilder.Entity<UserTeam>()
            .HasOne(ut => ut.team)
            .WithMany(t => t.user_team)
            .HasForeignKey(ut => ut.team_id);

        modelBuilder.Entity<Challenge>()
            .HasKey(c => c.id);

        modelBuilder.Entity<Challenge>()
            .HasOne(c => c.creator_team)
            .WithMany(t => t.creator_challenge)
            .HasForeignKey(c => c.creator_team_id);

        modelBuilder.Entity<Challenge>()
            .HasOne(c => c.target_team)
            .WithMany(t => t.target_challenge)
            .HasForeignKey(c => c.target_team_id);
    }
}