using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("sae_group")]
    public class SaeGroup
    {
        [Key]
        public Guid id { get; set; }

        public int id_sae { get; set; }
        public Sae sae { get; set; }

        public int id_group { get; set; }
        public Group group { get; set; }
    }
}
