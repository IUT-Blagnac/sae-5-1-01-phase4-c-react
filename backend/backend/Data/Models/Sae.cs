using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models
{
    [Table("sae")]
    public class Sae
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? min_student_per_group { get; set; }
        public int? max_student_per_group { get; set; }
        public int? min_group_per_subject { get; set; }
        public int? max_group_per_subject { get; set; }
        public State state { get; set; }

        [JsonIgnore]
        public List<Character> characters { get; set; }
        [JsonIgnore]
        public List<SaeGroup> sae_groups { get; set; }

        [JsonIgnore]
        public List<SaeCoach> sae_coachs { get; set; }

        [JsonIgnore]
        public List<Subject> subjects { get; set; }
    }
    public enum State
    {
        PENDING_USERS,
        PENDING_WISHES,
        LAUNCHED,
        LAUNCHED_OPEN_FOR_INTERNSHIP,
        CLOSED
    }
}
