using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Models;

[Table("role_user")]
public class RoleUser
{
    public int id { get; set; }
    public string name { get; set; }

    public List<User> users { get; set;  }
}