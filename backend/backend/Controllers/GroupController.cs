using System.Security.Claims;
using backend.Data;
using backend.Data.Models;
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
}
