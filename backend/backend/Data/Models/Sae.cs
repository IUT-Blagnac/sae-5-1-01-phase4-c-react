using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("sae")]
    public class Sae
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public int id_prom { get; set; }
        public Prom prom { get; set; } 

        public List<Subject> subjects { get; set; }
    }
}
