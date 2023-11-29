using backend.Data.Models;

namespace backend.Services.Interfaces;

public interface IUserService
{
    public User RegisterUser(string email, string passwd, string first_name, string last_name);
    public User? GetCurrentUser(HttpContext httpContext);
    public List<User> GetUsersByTeamId(Guid idTeam);
    public void RemoveUser(Guid id_user);
}