using backend.ApiModels.Output;
using backend.Data.Models;
using backend.FormModels;

namespace backend.Services.Interfaces;

public interface ITeamService
{
    public List<Team> GetTeamsBySaeId(Guid saeId);
    public OutputGetTeamByUserIdAndSaeId? GetTeamByUserIdAndSaeId(Guid userId, Guid saeId);
    public List<Team> GetTeams(Guid userId);
    public Team GetTeam(Guid id);
    public Team CreateTeam(TeamForm teamForm, Guid userId);
    public Team ModifyTeam(Guid id, TeamForm teamForm, Guid userId);
    public List<TeamWish> MakeWish(Guid user, Guid idTeam, Guid idSubject);
}