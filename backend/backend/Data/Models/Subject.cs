using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models
{
    [Table("subject")]
    public class Subject
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Guid id_sae { get; set; }

        [JsonIgnore]
        public Sae sae { get; set; }

        [JsonIgnore]
        public List<TeamWish> wish { get; set; }

        [JsonIgnore]
        public List<TeamSubject> team_subject { get; set; }

        [JsonIgnore]
        public List<SubjectCategory> subject_category { get; set; }
    }
}
