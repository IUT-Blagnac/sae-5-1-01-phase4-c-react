using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("user_team")]
    public class UserTeam
    {
        public Guid id_user { get; set; }
        public Guid id_team { get; set; }
        public string role { get; set; }
        
        public User user { get; set; }
        public Team team { get; set; } 
    }
}
