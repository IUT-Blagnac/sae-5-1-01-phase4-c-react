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

    public int role_id { get; set; }
    
    public RoleUser role_user { get; set; }

    public int? id_groupe { get; set; }
    public Group? group { get; set; }

}