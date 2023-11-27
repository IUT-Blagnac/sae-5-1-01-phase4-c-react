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
    private readonly IUserService _userService;
    private readonly IRoleUserService _roleUserService;

    public UserController(IUserService userService, IRoleUserService roleUserService)
    {
        _userService = userService;
        _roleUserService = roleUserService;
    }
    
    [HttpGet]
    [Route("currentUser")]
    public IActionResult GetAuthenticatedUser()
    {
        var currentUser = _userService.GetCurrentUser(HttpContext);

        if (currentUser is not null)
        {
            var role = _roleUserService.GetRole(currentUser.id_role);

            return new OkObjectResult( new { id = currentUser.id, email = currentUser.email, firstname = currentUser.first_name, lastname = currentUser.last_name, role = role.name});
        }
        else
        {
            return Unauthorized();
        }
    }
}