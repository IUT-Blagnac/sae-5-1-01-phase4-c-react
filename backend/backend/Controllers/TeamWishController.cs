using backend.Data.Models;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamWishController : ControllerBase
{
    private readonly ITeamWishService _teamWishService;

    public TeamWishController(ITeamWishService teamWishService)
    {
        _teamWishService = teamWishService;
    }

    [HttpPost]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<TeamWish>> AddTeamWish(Guid team_id, Guid subject_id)
    {
        try
        {
            _teamWishService.AddWish(team_id, subject_id);
        }
        catch (DbException)
        {
            return StatusCode(400, new { message = "Database error" });
        }
        catch
        {
            return StatusCode(400, new { message = "Unknown exception" });
        }

        return Ok();
    }

    [HttpDelete]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<TeamWish>> RemoveTeamWish(Guid team_id, Guid subject_id)
    {
        try
        {
            _teamWishService.RemoveWish(team_id, subject_id);
        }
        catch (DbException)
        {
            return StatusCode(400, new { message = "Database error" });
        }
        catch
        {
            return StatusCode(400, new { message = "Unknown exception" });
        }

        return Ok();
    }


    [HttpGet]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public ActionResult<List<TeamWish>> GetAllWishes()
    {
        return _teamWishService.GetWishes();
    }

    [HttpGet("{id_team}/{id_subject}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<TeamWish>> GetWish(Guid id_team, Guid id_subject)
    {
        return _teamWishService.GetWish(id_team, id_subject);
    }

    [HttpGet("team/{id}")]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public ActionResult<List<TeamWish>> GetWishesByTeam(Guid id_team)
    {
        return _teamWishService.GetWishesByTeamId(id_team);
    }

    [HttpGet("subject/{id}")]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public ActionResult<List<TeamWish>> GetWishesBySubject(Guid id_subject)
    {
        return _teamWishService.GetWishesByTeamId(id_subject);
    }
}