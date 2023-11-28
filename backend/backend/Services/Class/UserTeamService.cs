using backend.Data;
using backend.Data.Models;
using backend.Services.Interfaces;

namespace backend.Services.Class;

public class UserTeamService: IUserTeamService
{
    private readonly EntityContext _context;

    public UserTeamService(EntityContext context)
    {
        _context = context;
    }


    public UserTeam AddUserTeam(Guid id_user, Guid id_team, string role)
    {
        var userTeam = new UserTeam
        {
            id_user = id_user,
            id_team = id_team,
            role = role
        };

        _context.UserTeams.Add(userTeam);
        _context.SaveChangesAsync();

        return userTeam;
    }

    public void RemoveUserTeam(Guid id_user, Guid id_team)
    {
        var userTeam = _context.UserTeams.FirstOrDefault(x => x.id_team == id_team && x.id_user == id_user);

        _context.UserTeams.Remove(userTeam);
        _context.SaveChangesAsync();
    }
}