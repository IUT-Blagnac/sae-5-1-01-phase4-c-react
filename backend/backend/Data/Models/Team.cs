using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models
{
    [Table("team")]
    public class Team
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string color { get; set; }

        public Guid id_sae { get; set; }
        public Sae sae { get; set; }
        
        [JsonIgnore]
        public List<UserTeam> user_team { get; set; }
        [JsonIgnore]
        public List<Challenge> creator_challenge { get; set; }
        [JsonIgnore]
        public List<Challenge> target_challenge { get; set; }
        [JsonIgnore]
        public List<TeamWish> wish { get; set; }
        [JsonIgnore]
        public List<TeamSubject> team_subject { get; set; }
    }
}
