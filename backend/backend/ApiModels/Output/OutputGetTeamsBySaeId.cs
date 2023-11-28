using backend.Data.Models;

namespace backend.ApiModels.Output
{
    public class OutputGetTeamsBySaeId
    {
        public List<TeamComposition> teams { get; set; }
    }
    public class TeamComposition
    {
        public Guid idTeam { get; set; }
        public string nameTeam { get; set; }
        public string colorTeam { get; set; }
        public List<Guid> idUsers { get; set; }
    }
}
