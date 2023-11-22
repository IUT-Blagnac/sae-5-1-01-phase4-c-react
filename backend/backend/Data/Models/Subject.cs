using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("subject")]
    public class Subject
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
        public Guid id_category { get; set; }
        public Category category { get; set; }

        public int id_sae { get; set; }
        public Sae sae { get; set; }
        
        public List<TeamWish> wish { get; set; }
        public List<TeamSubject> team_subject { get; set; }
    }
}
