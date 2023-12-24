using backend.Data.Models;

namespace backend.ApiModels.Output
{
    public class OutputGetTeamByUserIdAndSaeId
    {
        public Guid idTeam { get; set; }
        public string nameTeam { get; set; }
        public string colorTeam { get; set; }
        public List<User> users { get; set; }
    }
}
