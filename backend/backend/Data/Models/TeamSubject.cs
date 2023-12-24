using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("team_subject")]
    public class TeamSubject
    {
        public Guid id_team { get; set; }
        public Guid id_subject { get; set; }
        
        public Team team { get; set; }
        public Subject subject { get; set; }
    }
}
