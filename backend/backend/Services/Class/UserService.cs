using backend.ApiModels.Output;
using backend.Data;
using backend.Data.Models;
using backend.Services.Interfaces;
using System.Security.Claims;

namespace backend.Services.Class;

public class UserService: IUserService
{
    private readonly EntityContext _context;

    public UserService(EntityContext context)
    {
        _context = context;
    }

    public User AddUser(string email, string passwd, string first_name, string last_name)
    {
        var new_user = new User
        {
            id = Guid.NewGuid(),
            email = email,
            password = passwd,
            first_name = first_name,
            last_name = last_name,
        };

        _context.Users.Add(new_user);
        _context.SaveChanges();

        return new_user;
    }

    public User? GetCurrentUser(HttpContext httpContext)
    {
        ClaimsIdentity? identity = httpContext.User.Identity as ClaimsIdentity;
        Claim? email = identity?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email);

        return email is not null ? _context.Users.FirstOrDefault(x => x.email == email.Value) :
                                   null;
    }

    public List<User> GetUsersByTeamId(Guid idTeam)
    {
        Team team = _context.Teams.FirstOrDefault(team => team.id == idTeam);
        var idUsers = _context.UserTeams.Where(u => u.id_team == team.id).Select(u => u.id_user);
        List<User> users = _context.Users.Where(u => idUsers.Contains(u.id)).ToList();

        return users;
    }

    public void RemoveUser(Guid id_user)
    {
        var user = _context.Users.FirstOrDefault(user => user.id == id_user);
        _context.Remove(user);
        _context.SaveChanges();
    }

    public OutputGetTeachers GetTeachers()
    {
        var output = new OutputGetTeachers();
        
        var idRoleTeacher = _context.Roles.Where(c => c.name == "Teacher").FirstOrDefault()?.id;

        if (idRoleTeacher is null)
        {
            return null;
        }

        var teachers = _context.Users.Where(user => user.id_role == idRoleTeacher).ToList();

        foreach (var teacher in teachers)
        {
            output.Teachers.Add(new TeacherSimplified
            {
                id = teacher.id,
                fullName = teacher.first_name + " " + teacher.last_name
            });
        }

        return output;
    }
}