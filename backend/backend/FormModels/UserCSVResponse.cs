using backend.Data.Models;

namespace backend.FormModels;

public class UserCSVResponse : UserCSVRegister
{
    public UserCSVResponse(User user)
    {
        FirstName = user.first_name;
        LastName = user.last_name;
        Email = user.email;
        Password = user.password;
    }

    public string Password { get; set; }


}