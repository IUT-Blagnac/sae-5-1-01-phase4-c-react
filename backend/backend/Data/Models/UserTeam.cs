using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models
{
    [Table("user_team")]
    public class UserTeam
    {
        public Guid id_user { get; set; }
        public Guid id_team { get; set; }
        public string role { get; set; }
        
        [JsonIgnore]
        public User user { get; set; }
        [JsonIgnore]
        public Team team { get; set; } 
    }
}
