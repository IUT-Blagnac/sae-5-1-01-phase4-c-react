using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("sae_coach")]
    public class SaeCoach
    {
        public Guid id_sae { get; set; }
        public Sae sae { get; set; }

        public Guid id_coach { get; set; }
        public User user { get; set; }
    }
}
