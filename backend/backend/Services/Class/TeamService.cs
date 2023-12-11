using System.Net;
using backend.ApiModels.Output;
using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;

namespace backend.Services.Class;

public class TeamService : ITeamService
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

    public OutputGetTeamByUserIdAndSaeId? GetTeamByUserIdAndSaeId(Guid userId, Guid saeId)
    {
        
        var team = (from u in _context.Users
                   join ut in _context.UserTeams on u.id equals ut.id_user
                   join t in _context.Teams on ut.id_team equals t.id
                   where u.id == userId && t.id_sae == saeId
                   select new OutputGetTeamByUserIdAndSaeId()
                   {
                       idTeam = t.id,
                       nameTeam = t.name,
                       colorTeam = t.color,
                   }).FirstOrDefault();

        if (team == null)
            return null;

        List<Guid> usersId = _context.UserTeams.Where(x => x.id_team == team.idTeam).Select(x => x.id_user).ToList();
        List<User> users = _context.Users.Where(x => usersId.Contains(x.id)).ToList();
        team.users = users;

        return team;
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
            color = teamForm.color,
            id_sae = teamForm.id_sae,
        };

        var userTeam = new UserTeam
        {
            id_user = userId,
            id_team = teamItem.id,
            role = "chef"
        };

        _context.Teams.Add(teamItem);
        _context.UserTeams.Add(userTeam);
        _context.SaveChanges();

        return teamItem;
    }

    public Team ModifyTeam(Guid id, TeamForm teamForm, Guid userId)
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

    public List<TeamWish> MakeWish(Guid idUser, Guid idTeam, Guid idSubject)
    {
        //verify if the user is part of the team
        var query = from t in _context.Teams
            join u in _context.UserTeams on t.id equals u.id_team 
            where u.id_user == idUser && u.id_team == idTeam
            select t;

        if (!query.Any())
        {
            throw new HttpRequestException("This user isn't part of the team", null, HttpStatusCode.Forbidden);
        }
        
        //verify if that the sae the team is part of has the subject and that the sae is in state pending wishes
        var query2 = from s in _context.Subjects
            join ut in _context.Teams on s.id_sae equals ut.id_sae
            join sae in _context.Saes on ut.id_sae equals sae.id
            where sae.state == State.PENDING_WISHES
            select s;
        
        if (!query2.Any())
        {
            throw new HttpRequestException("This sae subject isn't part of the teams SAE", null, HttpStatusCode.Forbidden);
        }
        
        //verify that a wish doesn't already exist for this subject
        var query3 = from w in _context.TeamWishes
            where w.id_team == idTeam && w.id_subject == idSubject
            select w;

        if (query3.Any())
        {
            throw new HttpRequestException("Wish already exists", null, HttpStatusCode.Forbidden);
        }

        var wish = new TeamWish()
        {
            id_subject = idSubject,
            id_team = idTeam,
        };
        


        List<TeamWish> wishes = query3.ToList();
        
        //wishes.Add(wish);
        
        
        _context.TeamWishes.Add(wish);

        _context.SaveChangesAsync();
        
        return wishes;
    }
}