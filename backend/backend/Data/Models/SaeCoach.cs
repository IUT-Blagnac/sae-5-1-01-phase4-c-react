using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("sae_coach")]
    public class SaeCoach
    {
        [Key]
        public Guid id { get; set; }

        public int id_sae { get; set; }
        public Sae sae { get; set; }

        public Guid id_coach { get; set; }
        public User user { get; set; }
    }
}
