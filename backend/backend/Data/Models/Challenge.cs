using System.ComponentModel.DataAnnotations.Schema;

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
        
        public Team creator_team { get; set; }
        public Team target_team { get; set; }
    }
}
