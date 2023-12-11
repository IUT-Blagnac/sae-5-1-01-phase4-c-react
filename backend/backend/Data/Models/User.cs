using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Models;

[Table("user")]
public class User
{
    public Guid id { get; set; }
    [EmailAddress]
    public string email { get; set; }

    [JsonIgnore]
    public string password { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }

    public int id_role { get; set; }
    
    public Role role_user { get; set; }
    
    [JsonIgnore]
    public List<UserTeam> user_team { get; set; }

    public Guid? id_group { get; set; }
    
    [JsonIgnore]
    public Group? group { get; set; }

    [JsonIgnore]
    public List<Character> characters { get; set; }
    
    [JsonIgnore]
    public List<SaeCoach> sae_coach { get; set; }
}