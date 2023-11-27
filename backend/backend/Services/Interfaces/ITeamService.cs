using backend.Data.Models;
using backend.FormModels;

namespace backend.Services.Interfaces;

public interface ITeamService
{
    public List<Team> GetTeamsBySaeId(Guid saeId);
    public List<Team> GetTeams(Guid userId);
    public Team GetTeam(Guid id);
    public Team CreateTeam(TeamForm teamForm, Guid userId);
    public Team MoifyTeam(Guid id, TeamForm teamForm, Guid userId);
}