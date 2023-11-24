using backend.Data.Models;

namespace backend.Services.Interfaces;

public interface IRoleUserService
{
    public Role? GetRole(int idRole);
}