using backend.Data.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet]
    [Authorize]
    public ActionResult<List<Group>> GetGroups()
    {
        var groups = _groupService.getGroups();

        if (groups == null)
        {
            return NotFound();
        }

        return groups;
    }
}