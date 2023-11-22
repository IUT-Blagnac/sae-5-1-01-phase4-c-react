using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models
{
    [Table("group")]
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool is_apprenticeship { get; set; }

        public int? id_group_parent { get; set; }
        public Group? group_parent { get; set; }
        public List<Group> groups_childs { get; set; }


        public List<User> users { get; set; }
        public List<SaeGroup> sae_groups { get; set; }
    }
}
