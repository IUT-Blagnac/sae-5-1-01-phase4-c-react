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

    [ForeignKey("id_role")]
    public RoleUser role_user { get; set; }

}