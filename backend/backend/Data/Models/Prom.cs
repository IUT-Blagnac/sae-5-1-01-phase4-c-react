using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("prom")]
    public class Prom
    {
        public int id { get; set; }
        public int year { get; set; }
        public string name { get; set; }


        public List<Sae> saes { get; set; }
        public List<Group> groups { get; set; }
    }
}
