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
            new() { id = new Guid("f441bf0a-115e-4c1e-8b1f-a7fb6f5738c5"), name = "1", is_apprenticeship = false },
            new() { id = new Guid("61cd9a7c-b555-4dea-92e8-9478bab248d8"), name = "2", is_apprenticeship = false },
            new() { id = new Guid("47c8a811-eb03-45cb-b366-136363f4903c"), name = "3", is_apprenticeship = true },
            new() { id = new Guid("6d7791c5-56dd-419f-aa7a-a811213cba32"), name = "3A", is_apprenticeship = true },
            new() { id = new Guid("2dbcecf8-c254-4cb7-9c55-0b3e7108054d"), name = "3B", is_apprenticeship = true },
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
                id = new Guid("55a48e75-9d05-4823-b5e8-941180602846"),
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
            new() { id = new Guid("21afc2e9-eca8-4bc0-aa47-ee71f1b8cf1c"), email = "pablo.seban@etu.univ-tlse2.fr", first_name = "Pablo", last_name = "Seban", id_role = Roles.Where(r => r.name == "Teacher").FirstOrDefault().id},
            new() { id = new Guid("589eb929-0623-48d3-b78c-0973f3cd9eef"), email = "remi.boulle@etu.univ-tlse2.fr", first_name = "Remi", last_name = "Boulle", id_role = Roles.Where(r => r.name == "Teacher").FirstOrDefault().id}
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
            new() { id = new Guid("f8f69fc6-2488-44f6-b742-3b63cb9cad6d"), email = "loan.gayrard@etu.univ-tlse2.fr", first_name = "Loan", last_name = "Gayrard", id_role = Roles.Where(r => r.name == "Student").FirstOrDefault().id, id_group = Groups.Where(g => g.name == "1").FirstOrDefault().id},
            new() { id = new Guid("ca7b4826-5d71-4d97-8dc5-b65a52a15f4c"), email = "matthieu.robert@etu.univ-tlse2.fr", first_name = "Matthieu", last_name = "Robert", id_role = Roles.Where(r => r.name == "Student").FirstOrDefault().id, id_group = Groups.Where(g => g.name == "2").FirstOrDefault().id},
            new() { id = new Guid("64f4f759-0c6e-4362-9495-0536ac3a512f"), email = "hugo.castell@etu.univ-tlse2.fr", first_name = "Hugo", last_name = "Castell", id_role = Roles.Where(r => r.name == "Student").FirstOrDefault().id, id_group = Groups.Where(g => g.name == "3A").FirstOrDefault().id}
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
            new() { id = new Guid("0e6890df-3199-4315-80fe-88fdfdb40671"), name = "Développement" }, new() { id = new Guid("28c1c623-12b1-4dac-87ae-2c95f9ecf598"), name = "Réseau" }, new() { id = new Guid("357a01a3-8895-4aa8-9d6c-d66d57ee01f2"), name = "Système" } 
        };

        foreach (var category in defaultCategories)
        {
            if (!Categories.Where(c => c.name == category.name).Any())
            {
                Categories.Add(category);
            }
        }

        SaveChanges();
        
        //DEFAULT SAE

        var defaultSaes = new List<Sae>()
        {
            new()
            {
                id = new Guid("d5e37380-8b99-49c8-8e89-723d8c5f7b97"), name = "SAE CaptElec",
                description = "SAE CaptElec desc", min_student_per_group = 2, max_student_per_group = 4,
                max_group_per_subject = 4, min_group_per_subject = 1, state = State.LAUNCHED
            },
            new()
            {
                id = new Guid("c5710c89-1b52-473b-886e-722f97ff713a"), name = "SAE Koh Lanta",
                description = "SAE Koh Lanta desc", min_student_per_group = 2, max_student_per_group = 6,
                max_group_per_subject = 7, min_group_per_subject = 2, state = State.PENDING_USERS
            },
        };

        foreach (var sae in defaultSaes)
        {
            if (!Saes.Where(c => c.name == sae.name).Any())
            {
                Saes.Add(sae);
            }
        }

        SaveChanges();
        
        // Link Sae To Group

        var defaultSaeGroup = new List<SaeGroup>()
        {
            new()
            {
                id_group = new Guid("f441bf0a-115e-4c1e-8b1f-a7fb6f5738c5"), id_sae =
                    new Guid("d5e37380-8b99-49c8-8e89-723d8c5f7b97")
            },
            new()
            {
                id_group = new Guid("47c8a811-eb03-45cb-b366-136363f4903c"), id_sae =
                    new Guid("d5e37380-8b99-49c8-8e89-723d8c5f7b97")
            },
            new()
            {
                id_group = new Guid("6d7791c5-56dd-419f-aa7a-a811213cba32"), id_sae =
                    new Guid("d5e37380-8b99-49c8-8e89-723d8c5f7b97")
            },
            new()
            {
                id_group = new Guid("f441bf0a-115e-4c1e-8b1f-a7fb6f5738c5"), id_sae =
                    new Guid("c5710c89-1b52-473b-886e-722f97ff713a")
            },
            new()
            {
                id_group = new Guid("6d7791c5-56dd-419f-aa7a-a811213cba32"), id_sae =
                    new Guid("c5710c89-1b52-473b-886e-722f97ff713a")
            },
        };

        foreach (var group in defaultSaeGroup)
        {
            if (!SaeGroups.Where(sg => sg.id_group == group.id_group && sg.id_sae == group.id_sae).Any())
            {
                SaeGroups.Add(group);
            }
        }

        SaveChanges();
        
        // Link Sae To Coach

        var defaultSaeCoach = new List<SaeCoach>()
        {
            new()
            {
                id_coach = new Guid("589eb929-0623-48d3-b78c-0973f3cd9eef"),
                id_sae = new Guid("d5e37380-8b99-49c8-8e89-723d8c5f7b97")
            },
            new()
            {
                id_coach = new Guid("21afc2e9-eca8-4bc0-aa47-ee71f1b8cf1c"),
                id_sae = new Guid("c5710c89-1b52-473b-886e-722f97ff713a")
            }
        };

        foreach (var saeCoach in defaultSaeCoach)
        {
            if (!SaeCoaches.Where(sc => sc.id_coach == saeCoach.id_coach && sc.id_sae == saeCoach.id_sae).Any())
            {
                SaeCoaches.Add(saeCoach);
            }
        }

        SaveChanges();
        
        // DEFAULT Character

        var defaultCharacter = new List<Character>()
        {
            new()
            {
                id = new Guid("6d96dc76-d479-4a44-8cd9-8b2b2b534119"), name = "Sonixray",
                id_sae = new Guid("d5e37380-8b99-49c8-8e89-723d8c5f7b97"),
                id_user = new Guid("f8f69fc6-2488-44f6-b742-3b63cb9cad6d")
            },
            new()
            {
                id = new Guid("e8f230f0-fffd-4ac6-8596-1e74bde54188"), name = "Aynbo",
                id_sae = new Guid("d5e37380-8b99-49c8-8e89-723d8c5f7b97"),
                id_user = new Guid("ca7b4826-5d71-4d97-8dc5-b65a52a15f4c")
            },
            new()
            {
                id = new Guid("0e3aaf7a-8cf3-46a2-b60d-e38a2f960506"), name = "Matz",
                id_sae = new Guid("c5710c89-1b52-473b-886e-722f97ff713a"),
                id_user = new Guid("ca7b4826-5d71-4d97-8dc5-b65a52a15f4c")
            }
        };

        foreach (var character in defaultCharacter) 
        {
            if (!Characters.Where(c => c.name == character.name).Any())
            {
                Characters.Add(character);
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