using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models
{
    [Table("category")]
    public class Category
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public List<SubjectCategory> subject_category { get; set; }
    }
}
