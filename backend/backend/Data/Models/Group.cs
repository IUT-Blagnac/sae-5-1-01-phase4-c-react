using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("group")]
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool is_apprenticeship { get; set; }

        public int? id_groupe_parent { get; set; }
        public Group? group_parent { get; set; }

        public int id_prom { get; set; }
        public Prom prom { get; set; } 

        public List<User> users { get; set; }
        public List<Group> groups_childs { get; set; }
    
    }
}
