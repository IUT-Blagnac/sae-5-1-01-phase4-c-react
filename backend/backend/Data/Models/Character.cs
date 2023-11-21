using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("character")]
    public class Character
    {
        [Key]
        public Guid id_character { get; set; }
        public string name { get; set; }

        public Guid id_user { get; set; }
        public User user { get; set; }

        public List<CharacterSkill> character_skills { get; set; }
    }
}
