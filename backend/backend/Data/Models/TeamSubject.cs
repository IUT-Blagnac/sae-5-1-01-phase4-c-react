using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("team_subject")]
    public class TeamSubject
    {
        public Guid team_id { get; set; }
        public Guid subject_id { get; set; }
        
        public Team team { get; set; }
        public Subject subject { get; set; }
    }
}
