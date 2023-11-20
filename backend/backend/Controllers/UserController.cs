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
    [Route("user")]
    [Authorize]
    public IActionResult GetAuthenticatedUser()
    {
        var currentUser = GetCurrentUser();

        if (currentUser != null)
        {
            return Ok($"Hi you are {currentUser.first_name}");
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