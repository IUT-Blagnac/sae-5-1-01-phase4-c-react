using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IRoleUserService _roleUserService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, IRoleUserService roleUserService, ILogger<UserController> logger)
    {
        _userService = userService;
        _roleUserService = roleUserService;
        _logger = logger;
    }

    [HttpGet("currentUser")]
    [AllowAnonymous]
    public IActionResult GetAuthenticatedUser()
    {
        var currentUser = _userService.GetCurrentUser(HttpContext);

        if (currentUser is not null)
        {
            var role = _roleUserService.GetRole(currentUser.id_role);

            return new OkObjectResult(new { id = currentUser.id, email = currentUser.email, firstname = currentUser.first_name, lastname = currentUser.last_name, role = role.name });
        }
        else
        {
            return Unauthorized();
        }
    }

    [HttpGet("teachers")]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public IActionResult GetTeachers()
    {
        try
        {
            var teachers = _userService.GetTeachers();
            return Ok(teachers);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
}