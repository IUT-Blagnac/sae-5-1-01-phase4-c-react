using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("skill")]
    public class Skill
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }

        public List<CharacterSkill> character_skills { get; set; }
    }
}
