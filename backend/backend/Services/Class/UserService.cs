using backend.ApiModels.Output;
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
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(EntityContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public class RegisterException : Exception
    {
        public int StatusCode { get; }
        public RegisterException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
    private string GenerateRandomPassword(int length)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/\\*£$_-";
        Random random = new Random();

        char[] randomArray = new char[length];
        for (int i = 0; i < length; i++)
        {
            randomArray[i] = chars[random.Next(chars.Length)];
        }

        return new string(randomArray);
    }

    private User RegisterUserWithoutSaving(string email, string? passwd, string first_name, string last_name, Guid id_group)
    {
        User? user = _context.Users.FirstOrDefault(x => x.email == email);

        Role? defaultRole = _context.Roles.FirstOrDefault(c => c.name == "Student");

        if (user is not null)
            throw new RegisterException(409, "User already exists");

        if (defaultRole is null)
            throw new RegisterException(500, "Internal server error");

        if (passwd is not null && passwd.Length < 8)
            throw new RegisterException(403, "Password is too short");

        var registered_user = new User
        {
            id = Guid.NewGuid(),
            email = email,
            first_name = first_name,
            last_name = last_name,
            role_user = defaultRole,
            id_group = id_group
        };

        if (passwd is null)
            passwd = GenerateRandomPassword(12);

        var hashedPassword = _passwordHasher.HashPassword(registered_user, passwd);

        registered_user.password = hashedPassword;

        _context.Users.Add(registered_user);

        registered_user.password = passwd;

        return registered_user;
    }

    public User RegisterUser(string email, string passwd, string first_name, string last_name, Guid id_group)
    {
        var registered_user = RegisterUserWithoutSaving(email, passwd, first_name, last_name, id_group);

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
                                                                last_name: userRegister.LastName,
                                                                userRegister.id_group);
            users.Add(new_registered_user);
        }

        _context.SaveChanges();

        return users;
    }

    public List<UserCSVResponse> RegisterUsers(IEnumerable<UserCSVRegister> userRegisters, Guid id_group)
    {
        List<User> users = new();

        foreach (UserCSVRegister userRegister in userRegisters)
        {
            var new_registered_user = RegisterUserWithoutSaving(email: userRegister.Email,
                                                                passwd: null, // Generated automatically
                                                                first_name: userRegister.FirstName,
                                                                last_name: userRegister.LastName,
                                                                id_group);
            users.Add(new_registered_user);
        }

        _context.SaveChanges();

        return users.Select(user => new UserCSVResponse(user)).ToList();
    }

    public User? GetUser(string? email)
    {
        return email is not null ? _context.Users.FirstOrDefault(x => x.email == email) :
                                   null;
    }

    public User? GetCurrentUser(HttpContext httpContext)
    {
        ClaimsIdentity? identity = httpContext.User.Identity as ClaimsIdentity;
        Claim? email = identity?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email);

        return GetUser(email?.Value);
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