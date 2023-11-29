using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace backend.Services.Class;

public class UserService : IUserService
{
    private readonly EntityContext _context;

    public UserService(EntityContext context)
    {
        _context = context;
    }

    public class RegisterException : Exception
    {
        public int StatusCode { get; }
        public RegisterException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    private User RegisterUserWithoutSaving(string email, string passwd, string first_name, string last_name)
    {
        User? user = _context.Users.FirstOrDefault(x => x.email == email);

        Role? defaultRole = _context.Roles.FirstOrDefault(c => c.name == "Student");

        if (user is not null)
            throw new RegisterException(409, "User already exits");

        if (defaultRole is null)
            throw new RegisterException(500, "Internal server error");

        if (passwd.Length < 8)
            throw new RegisterException(403, "Password is too short");

        var registered_user = new User
        {
            id = Guid.NewGuid(),
            email = email,
            first_name = first_name,
            last_name = last_name,
            role_user = defaultRole
        };

        var hashedPassword = new PasswordHasher<User>().HashPassword(registered_user, passwd);

        registered_user.password = hashedPassword;

        _context.Users.Add(registered_user);

        return registered_user;
    }

    public User RegisterUser(string email, string passwd, string first_name, string last_name)
    {
        var registered_user = RegisterUserWithoutSaving(email, passwd, first_name, last_name);

        _context.SaveChanges();

        return registered_user;
    }

    public List<User> RegisterUsers(IEnumerable<UserRegister> userRegisters)
    {
        List<User> users = new();

        foreach (UserRegister userRegister in userRegisters)
        {
            var new_registered_user = RegisterUserWithoutSaving(email: userRegister.Email,
                                                                passwd: userRegister.Password,
                                                                first_name: userRegister.FirstName,
                                                                last_name: userRegister.LastName);
            users.Add(new_registered_user);
        }

        _context.SaveChanges();

        return users;
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

}