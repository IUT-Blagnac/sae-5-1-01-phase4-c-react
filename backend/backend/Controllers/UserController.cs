using System.Security.Claims;
using backend.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    [HttpGet]
    [Route("user")]
    [Authorize]
    public IActionResult GetAuthenticatedUser()
    {
        var currentUser = GetCurrentUser();

        if (currentUser != null)
        {
            return Ok($"Hi you are {currentUser.Username}");
        }

        return Unauthorized();
    }

    private User? GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;
            return new User
            {
                Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Password = "hidden"
            };
        }

        return null;
    }
}