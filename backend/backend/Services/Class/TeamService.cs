using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Class;

public class TeamService: ITeamService
{
    private readonly EntityContext _context;

    public TeamService(EntityContext context)
    {
        _context = context;
    }

    public List<Team> GetTeamsBySaeId(Guid saeId)
    {
        var teams = _context.Teams.Where(x => x.id_sae == saeId).ToList();
        return teams;
    }
    
    public List<Team> GetTeams(Guid userId)
    {

        var query = (from u in _context.Users
            join ut in _context.UserTeams on u.id equals ut.id_user
            join t in _context.Teams on ut.id_team equals t.id
            where u.id == userId
            select new Team()
            {
                id = t.id,
                name = t.name,
                color = t.color
            }).ToList();

        return query;
    }

    public Team GetTeam(Guid id)
    {
        return _context.Teams.Find(id);
    }

    public Team CreateTeam(TeamForm teamForm, Guid userId)
    {
        var teamItem = new Team
        {
            id = Guid.NewGuid(),
            name = teamForm.name,
            color = teamForm.color
        };

        var userTeam = new UserTeam
        {
            id_user = userId,
            id_team = teamItem.id,
            role = "chef"
        };
        
        _context.Teams.Add(teamItem);
        _context.UserTeams.Add(userTeam);
        _context.SaveChangesAsync();

        return teamItem;
    }

    public Team MoifyTeam(Guid id, TeamForm teamForm, Guid userId)
    {
        var userTeam = _context.UserTeams.FirstOrDefault(x => x.id_team == id && x.id_user == userId);

        if (userTeam == null)
            return null;

        var team = _context.Teams.Find(id);

        if (team == null)
            return null;

        team.name = teamForm.name;
        team.color = teamForm.color;

        _context.SaveChangesAsync();

        return team;
    }
}