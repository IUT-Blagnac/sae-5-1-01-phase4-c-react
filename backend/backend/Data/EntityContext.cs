using backend.Data.Models;
using backend.FormModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace backend.Data;

public class EntityContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterSkill> CharacterSkills { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Sae> Saes { get; set; }
    public DbSet<SaeCoach> SaeCoaches { get; set; }
    public DbSet<SaeGroup> SaeGroups { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SubjectCategory> SubjectCategories { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamSubject> TeamSubjects { get; set; }
    public DbSet<TeamWish> TeamWishes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserTeam> UserTeams { get; set; }
    
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

        var defaultRoles = new List<Role>
        {
            new() { name = "Student" },
            new() { name = "Teacher" },
            new() { name = "Admin" },
        };

        foreach (var role in defaultRoles)
        {
            if (!Roles.Where(c => c.name == role.name).Any())
            {
                Roles.Add(role);
            }
        }

        SaveChanges();

        //DEFAULT GROUPS

        var defaultGroups = new List<Group>
        {
            new() { name = "1", is_apprenticeship = false },
            new() { name = "2", is_apprenticeship = false },
            new() { name = "3", is_apprenticeship = true },
            new() { name = "3A", is_apprenticeship = true },
            new() { name = "3B", is_apprenticeship = true },
        };

        foreach(var group in defaultGroups)
        {
            if (!Groups.Where(c => c.name == group.name).Any())
            {
                if (group.name == "3A" || group.name == "3B")
                {
                    group.group_parent = Groups.Where(c => c.name == "3").FirstOrDefault();
                }
                Groups.Add(group);
            }
        }

        SaveChanges();

        //DEFAULT ADMIN USER

        if (!Users.Where(c => c.email == "admin@superadmin.com").Any())
        {
            var defaultAdmin = new User
            {
                id = new Guid("SUPERULTRAADMIN"),
                email = "admin@superadmin.com",
                role_user = Roles.Where(c => c.name == "Admin").FirstOrDefault(),
                first_name = "Admin",
                last_name = "SuperAdmin",
            };

            defaultAdmin.password = _passwordHasher.HashPassword(defaultAdmin, "isfqzA8@&Ne@y9Ls@9CK");
            Users.Add(defaultAdmin);
        }

        SaveChanges();

        //DEFAULT TEACHERS

        var defaultTeachers = new List<User>
        {
            new() { id = new Guid("PABLOSEBAN"), email = "pablo.seban@etu.univ-tlse2.fr", first_name = "Pablo", last_name = "Seban", id_role = Roles.Where(r => r.name == "Teacher").FirstOrDefault().id},
            new() { id = new Guid("REMIBOULLE"), email = "remi.boulle@etu.univ-tlse2.fr", first_name = "Remi", last_name = "Boulle", id_role = Roles.Where(r => r.name == "Teacher").FirstOrDefault().id}
        };

        foreach (var teacher in defaultTeachers)
        {
            if (!Users.Where(c => c.email == teacher.email).Any())
            {
                teacher.password = _passwordHasher.HashPassword(teacher, "d4GRPdJ&c8kdMQtSP?S5");
                Users.Add(teacher);
            }
        }

        SaveChanges();

        //DEFAULT USERS

        var defaultStudents = new List<User>
        {
            new() { id = new Guid("LOANGAYRARD"), email = "loan.gayrard@etu.univ-tlse2.fr", first_name = "Loan", last_name = "Gayrard", id_role = Roles.Where(r => r.name == "Student").FirstOrDefault().id, id_group = Groups.Where(g => g.name == "1").FirstOrDefault().id},
            new() { id = new Guid("MATTHIEUROBERT"), email = "matthieu.robert@etu.univ-tlse2.fr", first_name = "Matthieu", last_name = "Robert", id_role = Roles.Where(r => r.name == "Student").FirstOrDefault().id, id_group = Groups.Where(g => g.name == "2").FirstOrDefault().id},
            new() { id = new Guid("HUGOCASTELL"), email = "hugo.castell@etu.univ-tlse2.fr", first_name = "Hugo", last_name = "Castell", id_role = Roles.Where(r => r.name == "Student").FirstOrDefault().id, id_group = Groups.Where(g => g.name == "3A").FirstOrDefault().id}
        };

        foreach (var student in defaultStudents)
        {
            if (!Users.Where(c => c.email == student.email).Any())
            {
                student.password = _passwordHasher.HashPassword(student, "6@ANdqS$x@er&76khmkJ");
                Users.Add(student);
            }
        }

        SaveChanges();

        //DEFAULT CATEGORIES

        var defaultCategories = new List<Category>()
        {
            new() { id = new Guid("DEVELOPMENT"), name = "Développement" }, new() { id = new Guid("NETWORK"), name = "Réseau" }, new() { id = new Guid("SYSTEM"), name = "Système" } 
        };

        foreach (var category in defaultCategories)
        {
            if (!Categories.Where(c => c.name == category.name).Any())
            {
                Categories.Add(category);
            }
        }

        SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DatabaseConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // #-----------#
        // # Role User #
        // #-----------#
        modelBuilder.Entity<Role>()
            .HasKey(ru => ru.id);
        
        modelBuilder.Entity<Role>()
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
            .HasKey(ut => new { ut.id_sae, ut.id_coach});

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
            .HasKey(ut => new { ut.id_sae, ut.id_group });

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

        // #----------#
        // # UserTeam #
        // #----------#
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

        // #-----------#
        // # Challenge #
        // #-----------#
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

        // #---------#
        // # Subject #
        // #---------#

        modelBuilder.Entity<Subject>()
            .HasKey(s => s.id);

        modelBuilder.Entity<Subject>()
            .HasOne(s => s.sae)
            .WithMany(c => c.subjects)
            .HasForeignKey(s => s.id_sae);

        // #----------#
        // # Teamwish #
        // #----------#
        modelBuilder.Entity<TeamWish>()
            .HasKey(ts => new { ts.id_team, ts.id_subject });

        modelBuilder.Entity<TeamWish>()
            .HasOne(w => w.team)
            .WithMany(t => t.wish)
            .HasForeignKey(w => w.id_team);

        modelBuilder.Entity<TeamWish>()
            .HasOne(w => w.subject)
            .WithMany(s => s.wish)
            .HasForeignKey(w => w.id_subject);

        // #-------------#
        // # TeamSubject #
        // #-------------#
        modelBuilder.Entity<TeamSubject>()
            .HasKey(ts => new { ts.id_team, ts.id_subject });

        modelBuilder.Entity<TeamSubject>()
            .HasOne(ts => ts.subject)
            .WithMany(s => s.team_subject)
            .HasForeignKey(ts => ts.id_subject);

        modelBuilder.Entity<TeamSubject>()
            .HasOne(ts => ts.team)
            .WithMany(t => t.team_subject)
            .HasForeignKey(ts => ts.id_team);

        // #----------#
        // # Category #
        // #----------#
        modelBuilder.Entity<Category>()
            .HasKey(c => c.id);

        // #-----------------#
        // # SubjectCategory #
        // #-----------------#
        modelBuilder.Entity<SubjectCategory>()
            .HasKey(ts => new { ts.id_subject, ts.id_category});

        modelBuilder.Entity<SubjectCategory>()
            .HasOne(c => c.subject)
            .WithMany(s => s.subject_category)
            .HasForeignKey(c => c.id_subject);

        modelBuilder.Entity<SubjectCategory>()
            .HasOne(c => c.category)
            .WithMany(s => s.subject_category)
            .HasForeignKey(c => c.id_category);

        // #----------------#
        // # CharacterSkill #
        // #----------------#
        modelBuilder.Entity<CharacterSkill>()
            .HasKey(ts => new { ts.id_character, ts.id_skill });

        modelBuilder.Entity<CharacterSkill>()
            .HasOne(c => c.character)
            .WithMany(c => c.character_skills)
            .HasForeignKey(c => c.id_character);

        modelBuilder.Entity<CharacterSkill>()
            .HasOne(c => c.skill)
            .WithMany(s => s.character_skills)
            .HasForeignKey(c => c.id_skill);

        // #-----------#
        // # Character #
        // #-----------#
        modelBuilder.Entity<Character>()
            .HasOne(c => c.user)
            .WithMany(u => u.characters)
            .HasForeignKey(c => c.id_user);

        modelBuilder.Entity<Character>()
            .HasOne(c => c.sae)
            .WithMany(u => u.characters)
            .HasForeignKey(c => c.id_sae);
    }
}