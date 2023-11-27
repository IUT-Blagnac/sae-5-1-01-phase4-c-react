using backend.Data.Models;

namespace backend.Services.Interfaces;

public interface IUserService
{
    public User AddUser(string mail, string passwd, string name, string surname);
    public User? GetCurrentUser(HttpContext httpContext);
    public List<User> GetUsersByTeamId(Guid idTeam);
    public void RemoveUser(Guid id_user);
}