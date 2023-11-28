using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models
{
    [Table("subject_category")]
    public class SubjectCategory
    {
        public Guid id_subject { get; set; }
        public Subject subject { get; set; }
        public Guid id_category { get; set; }
        public Category category { get; set; }
    }
}
