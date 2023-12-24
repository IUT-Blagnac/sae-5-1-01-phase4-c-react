﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("character_skill")]
    public class CharacterSkill
    {
        public Guid id_character { get; set; }
        public Character character { get; set; }

        public Guid id_skill { get; set; }
        public Skill skill { get; set; }

        [Range(1, 5)]
        public int confidence_level { get; set; }
    }
}
