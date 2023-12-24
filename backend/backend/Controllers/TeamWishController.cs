using backend.ApiModels.Input;
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

    /// <summary>
    /// Adds a <see cref="TeamWish"/>
    /// </summary>
    /// <param name="subject_id">Subject id</param>
    /// <param name="team_id">Team id</param>
    /// <response code="200">TeamWish has been added successfully</response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    [HttpPost]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<TeamWish>> AddTeamWish([FromBody] InputTeamWish teamWish)
    {
        try
        {
            _teamWishService.AddWish(teamWish.team_id, teamWish.subject_id);
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

    /// <summary>
    /// Delete a <see cref="TeamWish"/>
    /// </summary>
    /// <response code="200">TeamWish has been deleted successfully</response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    [HttpDelete()]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<TeamWish>> RemoveTeamWish([FromBody] InputTeamWish teamWish)
    {
        try
        {
            _teamWishService.RemoveWish(teamWish.team_id, teamWish.subject_id);
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

    /// <summary>
    /// Lists all the wishes of the teams as <see cref="TeamWish"/>
    /// </summary>
    /// <returns>The wishes of the teams</returns>
    /// <response code="200">The team wishes as <see cref="TeamWish"/></response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Teacher access minimum]</response>
    [HttpGet]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public ActionResult<List<TeamWish>> GetAllWishes()
    {
        return _teamWishService.GetWishes();
    }

    /// <summary>
    /// Return a specific <see cref="TeamWish"/>
    /// </summary>
    /// <param name="id_subject">Subject id</param>
    /// <param name="id_team">Team id</param>
    /// <returns>A <see cref="TeamWish"/> corresponding to the parameters</returns>
    /// <response code="200">Returns the <see cref="TeamWish"/></response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    [HttpGet("{id_team}/{id_subject}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<TeamWish>> GetWish(Guid id_team, Guid id_subject)
    {
        return _teamWishService.GetWish(id_team, id_subject);
    }

    /// <summary>
    /// Return the list of <see cref="TeamWish"/> of the team
    /// </summary>
    /// <param name="id_team">Team id</param>
    /// <returns>A <see cref="TeamWish"/> corresponding to the team</returns>
    /// <response code="200">Returns the list of <see cref="TeamWish"/> of the team</response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Teacher access minimum]</response>
    [HttpGet("team/{id_team}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<TeamWish>> GetWishesByTeam(Guid id_team)
    {
        return _teamWishService.GetWishesByTeamId(id_team);
    }

    /// <summary>
    /// Return the <see cref="TeamWish"/> of the subject
    /// </summary>
    /// <param name="id_subject">Subject id</param>
    /// <returns>A <see cref="TeamWish"/> corresponding to the subject</returns>
    /// <response code="200">Returns the <see cref="TeamWish"/></response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Teacher access minimum]</response>
    [HttpGet("subject/{id}")]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public ActionResult<List<TeamWish>> GetWishesBySubject(Guid id_subject)
    {
        return _teamWishService.GetWishesByTeamId(id_subject);
    }
}