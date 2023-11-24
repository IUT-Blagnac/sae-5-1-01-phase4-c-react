using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models
{
    [Table("challenge")]
    public class Challenge
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
        public Guid creator_team_id { get; set; }
        public Guid target_team_id { get; set; }
        
        [JsonIgnore]
        public Team creator_team { get; set; }
        [JsonIgnore]
        public Team target_team { get; set; }
        public bool completed { get; set; }
    }
}
