using backend.Data.Models;

namespace backend.FormModels;

public class UserRegister : UserLogin
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid id_group { get; set; }
}