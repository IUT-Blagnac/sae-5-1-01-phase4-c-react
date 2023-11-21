using System.Security.Claims;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly EntityContext _context;

    public UserController(EntityContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [Route("currentUser")]
    [Authorize]
    public IActionResult GetAuthenticatedUser()
    {
        var currentUser = GetCurrentUser();

        var role = _context.RoleUsers.FirstOrDefault(x => x.id == currentUser.role_id);
        
        if (currentUser != null)
        {
            return new OkObjectResult( new { email = currentUser.email, firstname = currentUser.first_name, lastname = currentUser.last_name, role = role.name});
        }

        return Unauthorized();
    }

    private User? GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            
            return _context.Users.FirstOrDefault(x => x.email == email);
        }

        return null;
    }
}