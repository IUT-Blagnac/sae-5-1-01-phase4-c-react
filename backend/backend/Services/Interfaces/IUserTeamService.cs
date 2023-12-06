using backend.Data.Models;

namespace backend.Services.Interfaces;

public interface IUserTeamService
{
    public UserTeam AddUserTeam(Guid id_user, Guid id_team, string role);
    public void RemoveUserTeam(Guid id_user, Guid id_team);
    public List<UserTeam> GenTeams(List<Guid> users, List<Guid> teams);
}