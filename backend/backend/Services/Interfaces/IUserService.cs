using backend.ApiModels.Output;
using backend.Data.Models;
using backend.FormModels;

namespace backend.Services.Interfaces;

public interface IUserService
{
    public List<User> RegisterUsers(IEnumerable<UserRegister> userRegisters);
    public User RegisterUser(string email, string passwd, string first_name, string last_name);
    public User? GetCurrentUser(HttpContext httpContext);
    public List<User> GetUsersByTeamId(Guid idTeam);
    public void RemoveUser(Guid id_user);
    public OutputGetTeachers GetTeachers();
}