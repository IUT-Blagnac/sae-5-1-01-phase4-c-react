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

    /// <summary>
    /// Get the authenticated user of the http context (<see cref="HttpContext"/>)
    /// </summary>
    /// <returns>List of the teachers as <see cref="ApiModels.Output.OutputGetTeachers"/></returns>
    /// <response code="200">The authenticated user of the <see cref="HttpContext"/></response>
    /// <response code="401">User not authenticated</response>
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

    /// <summary>
    /// Lists all the teachers of the base
    /// </summary>
    /// <returns>List of the teachers as <see cref="ApiModels.Output.OutputGetTeachers"/></returns>
    /// <response code="200">Returns the teachers</response>
    /// <response code="401">Not authorized to access this method. [Teacher access minimum]</response>
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