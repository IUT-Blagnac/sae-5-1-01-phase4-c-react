using backend.Data;
using backend.Data.Models;
using backend.Services.Interfaces;
using System.Security.Claims;

namespace backend.Services.Class;

public class RoleUserService : IRoleUserService
{
    private readonly EntityContext _context;

    public RoleUserService(EntityContext context)
    {
        _context = context;
    }

    public Role? GetRole(int idRole)
    {
        return _context.RoleUsers.FirstOrDefault(roleuser => roleuser.id == idRole);
    }
}