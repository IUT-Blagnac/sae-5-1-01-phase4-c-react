using backend.Data.Models;
using backend.FormModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace backend.Data;

public class EntityContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RoleUser> RoleUsers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<UserTeam> UserTeams { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<TeamWish> Wishes { get; set; }
    public DbSet<TeamSubject> TeamSubjects { get; set; }
    public DbSet<Category> Categories { get; set; }
    
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
            .HasForeignKey(u => u.id_role);

        modelBuilder.Entity<User>()
            .HasOne(u => u.group)
            .WithMany(g => g.users)
            .HasForeignKey(u => u.id_group);

        // #-------#
        // # Group #
        // #-------#
        modelBuilder.Entity<Group>()
            .HasKey(u => u.id);

        modelBuilder.Entity<Group>()
            .HasOne(g => g.group_parent)
            .WithMany(g => g.groups_childs)
            .HasForeignKey(g => g.id_group_parent);

        // #-----#
        // # Sae #
        // #-----#
        modelBuilder.Entity<Sae>()
            .HasKey(u => u.id);

        // #-----------#
        // # Sae Coach #
        // #-----------#
        modelBuilder.Entity<SaeCoach>()
            .HasOne(c => c.user)
            .WithMany(u => u.sae_coach)
            .HasForeignKey(c => c.id_coach);

        modelBuilder.Entity<SaeCoach>()
            .HasOne(c => c.sae)
            .WithMany(s => s.sae_coachs)
            .HasForeignKey(c => c.id_sae);

        // #-----------#
        // # Sae Group #
        // #-----------#
        modelBuilder.Entity<SaeGroup>()
            .HasOne(g => g.group)
            .WithMany(g => g.sae_groups)
            .HasForeignKey(g => g.id_group);

        modelBuilder.Entity<SaeGroup>()
            .HasOne(g => g.sae)
            .WithMany(s => s.sae_groups)
            .HasForeignKey(g => g.id_sae);

        // #------#
        // # Team #
        // #------#
        modelBuilder.Entity<Team>()
            .HasKey(c => c.id);

        modelBuilder.Entity<UserTeam>()
            .HasKey(ut => new { ut.id_user, ut.id_team });
            
        modelBuilder.Entity<UserTeam>()
            .HasOne(ut => ut.user)
            .WithMany(u => u.user_team)
            .HasForeignKey(ut => ut.id_user);

        modelBuilder.Entity<UserTeam>()
            .HasOne(ut => ut.team)
            .WithMany(t => t.user_team)
            .HasForeignKey(ut => ut.id_team);

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

        modelBuilder.Entity<Subject>()
            .HasKey(s => s.id);

        modelBuilder.Entity<TeamWish>()
            .HasKey(w => w.id);

        modelBuilder.Entity<TeamWish>()
            .HasOne(w => w.team)
            .WithMany(t => t.wish)
            .HasForeignKey(w => w.id_team);

        modelBuilder.Entity<TeamWish>()
            .HasOne(w => w.subject)
            .WithMany(s => s.wish)
            .HasForeignKey(w => w.id_subject);

        modelBuilder.Entity<TeamSubject>()
            .HasKey(ts => new { ts.subject_id, ts.team_id });

        modelBuilder.Entity<TeamSubject>()
            .HasOne(ts => ts.subject)
            .WithMany(s => s.team_subject)
            .HasForeignKey(ts => ts.subject_id);

        modelBuilder.Entity<TeamSubject>()
            .HasOne(ts => ts.team)
            .WithMany(t => t.team_subject)
            .HasForeignKey(ts => ts.team_id);

        modelBuilder.Entity<Category>()
            .HasKey(c => c.id);

        modelBuilder.Entity<Subject>()
            .HasOne(s => s.category)
            .WithMany(c => c.subject)
            .HasForeignKey(s => s.id_category);

        modelBuilder.Entity<Subject>()
            .HasOne(s => s.sae)
            .WithMany(c => c.subjects)
            .HasForeignKey(s => s.id_sae);

        modelBuilder.Entity<CharacterSkill>()
            .HasOne(c => c.character)
            .WithMany(c => c.character_skills)
            .HasForeignKey(c => c.id_character);

        modelBuilder.Entity<CharacterSkill>()
            .HasOne(c => c.skill)
            .WithMany(s => s.character_skills)
            .HasForeignKey(c => c.id_skill);

        modelBuilder.Entity<Character>()
            .HasOne(c => c.user)
            .WithMany(u => u.characters)
            .HasForeignKey(c => c.id_user);
    }
}