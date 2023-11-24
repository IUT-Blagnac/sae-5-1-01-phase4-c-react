using System.Security.Claims;
using backend.Data;
using backend.Data.Models;
using backend.Services.Class;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly EntityContext _context;
    private readonly IUserService _userService;

    public UserController(EntityContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    
    [HttpGet]
    [Route("currentUser")]
    [Authorize]
    public IActionResult GetAuthenticatedUser()
    {
        var currentUser = _userService.GetCurrentUser(HttpContext);

        if (currentUser is not null)
        {
            // Recherche du role
            var role = _context.RoleUsers.FirstOrDefault(x => x.id == currentUser.id_role);

            // Retour d'un objet OK
            return new OkObjectResult( new { email = currentUser.email, firstname = currentUser.first_name, lastname = currentUser.last_name, role = role.name});
        }
        else
        {
            // Non autorisé
            return Unauthorized();
        }
    }
}