using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("equipe")]
    public class Team
    {
        public Guid id { get; set; }
        public string name { get; set; }
    }
}
