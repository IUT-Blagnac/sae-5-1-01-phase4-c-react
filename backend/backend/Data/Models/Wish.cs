using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("wish")]
    public class Wish
    {
        public Guid id { get; set; }
        public Guid team_id { get; set; }
        public Guid subject_id { get; set; }
        
        public Team team { get; set; }
        public Subject subject { get; set; }
    }
}
