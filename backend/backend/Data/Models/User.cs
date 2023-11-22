using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models;

[Table("user")]
public class User
{
    public Guid id { get; set; }
    [EmailAddress]
    public string email { get; set; }
    public string password { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }

    public int id_role { get; set; }
    
    public Role role_user { get; set; }
    public List<UserTeam> user_team { get; set; }

    public int? id_group { get; set; }
    public Group? group { get; set; }

    public List<Character> characters { get; set; }
    public List<SaeCoach> sae_coach { get; set; }
}