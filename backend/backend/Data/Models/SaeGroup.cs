using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("sae_group")]
    public class SaeGroup
    {
        public Guid id_sae { get; set; }
        public Sae sae { get; set; }

        public Guid id_group { get; set; }
        public Group group { get; set; }
    }
}
