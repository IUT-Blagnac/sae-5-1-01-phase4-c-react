using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("category")]
    public class Category
    {
        public Guid id { get; set; }
        public string name { get; set; }
        
        public List<Subject> subject { get; set; }
    }
}
