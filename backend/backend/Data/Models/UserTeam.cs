using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("user_equipe")]
    public class UserTeam
    {
        public Guid user_id { get; set; }
        public Guid team_id { get; set; }
        public string role { get; set; }
        
        public User user { get; set; }
        public Team team { get; set; } 
    }
}
