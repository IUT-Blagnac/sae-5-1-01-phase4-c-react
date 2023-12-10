using System.Security.Claims;
using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Class;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController: ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly ILogger<GroupController> _logger;

    public GroupController(IGroupService groupService, ILogger<GroupController> logger)
    {
        _groupService = groupService;
        _logger = logger;
    }

    /// <summary>
    /// Get all groups
    /// Note: Only teachers can access this route
    /// </summary>
    /// <returns>A list of all groups</returns>
    /// <response code="200">Returns a list of all groups</response>
    /// <response code="401">If the user is not a teacher</response>
    [HttpGet]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public IActionResult GetGroups()
    {
        try
        {
            var groups = _groupService.GetGroups();
            return Ok(groups);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Create a new group by giving a name and a boolean to know if it's an apprenticeship group or not
    /// Note: Only teachers can access this route
    /// </summary>
    /// <param name="group"></param>
    /// <returns>The created group</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /group
    ///     {
    ///     "name": "test",
    ///     "is_apprenticeship": true
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Returns the created group</response>
    /// <response code="401">If the user is not a teacher</response>
    [HttpPost]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public IActionResult CreateGroup([FromBody] GroupForm group)
    {
        try
        {
            var new_group = _groupService.CreateGroup(group.name, group.is_apprenticeship);
            return Ok(new_group);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest();
        }
    }
}
