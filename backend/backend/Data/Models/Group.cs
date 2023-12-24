using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models
{
    [Table("group")]
    public class Group
    {
        public Guid id { get; set; }
        public string name { get; set; }

        [JsonIgnore]
        public bool is_apprenticeship { get; set; }

        [JsonIgnore]
        public Guid? id_group_parent { get; set; }

        [JsonIgnore]
        public Group? group_parent { get; set; }

        [JsonIgnore]
        public List<Group> groups_childs { get; set; }

        [JsonIgnore]
        public List<User> users { get; set; }
        
        [JsonIgnore]
        public List<SaeGroup> sae_groups { get; set; }
    }
}
