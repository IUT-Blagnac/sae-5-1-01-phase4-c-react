using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models
{
    [Table("skill")]
    public class Skill
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }

        [JsonIgnore]
        public List<CharacterSkill> character_skills { get; set; }
    }
}
