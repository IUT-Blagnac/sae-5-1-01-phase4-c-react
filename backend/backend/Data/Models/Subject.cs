using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("subject")]
    public class Subject
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
        public Guid category_id { get; set; }
        public Category category { get; set; }
        
        public List<Wish> wish { get; set; }
        public List<TeamSubject> team_subject { get; set; }
    }
}
