using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("team")]
    public class Team
    {
        public Guid id { get; set; }
        public string name { get; set; }
        
        public List<UserTeam> user_team { get; set; }
        public List<Challenge> creator_challenge { get; set; }
        public List<Challenge> target_challenge { get; set; }
        
        public List<Wish> wish { get; set; }
        public List<TeamSubject> team_subject { get; set; }
    }
}
