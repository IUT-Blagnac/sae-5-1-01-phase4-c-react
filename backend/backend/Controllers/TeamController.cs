using backend.ApiModels.Output;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly IUserService _userService;

    public TeamController(ITeamService teamService, IUserService userService)
    {
        _teamService = teamService;
        _userService = userService;
    }

    /// <summary>
    /// Returns a list of all the teams as <see cref="Team"/> 
    /// </summary>
    /// <returns>A list of <see cref="Team"/></returns>
    /// <response code="200">Returns a liste of all the <see cref="Team"/></response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    [HttpGet]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Team>> GetTeams()
    {
        var user = _userService.GetCurrentUser(HttpContext);

        return _teamService.GetTeams(user.id);
    }

    /// <summary>
    /// Returns the <see cref="Team"/> corresponding to the id
    /// </summary>
    /// <param name="id">Team id</param>
    /// <returns>A <see cref="Team"/></returns>
    /// <response code="200">Returns the <see cref="Team"/></response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    [HttpGet("{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<Team>> GetTeam(Guid id)
    {
        var team = _teamService.GetTeam(id);

        if (team == null)
        {
            return NotFound();
        }

        return team;
    }

    /// <summary>
    /// Returns the <see cref="Team"/> corresponding to the user id on a sae id
    /// </summary>
    /// <param name="user_id">User id</param>
    /// <param name="sae_id">SAE id</param>
    /// <returns>A <see cref="Team"/> or null</returns>
    /// <response code="200">Returns the <see cref="Team"/> or null</response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    [HttpGet("{user_id}/{sae_id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public Team? GetTeamByUserIdAndSaeId(Guid user_id, Guid sae_id)
    {
        return _teamService.GetTeamByUserIdAndSaeId(user_id, sae_id);
    }

    /// <summary>
    /// Create a <see cref="Team"/> corresponding to <see cref="TeamForm"/> informations
    /// </summary>
    /// <param name="teamForm">Infos to create a <see cref="Team"/></param>
    /// <returns>A <see cref="Team"/></returns>
    /// <response code="200">Returns the <see cref="Team"/></response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    [HttpPost]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<Team>> CreateTeam(TeamForm teamForm)
    {
        var user = _userService.GetCurrentUser(HttpContext);

        var teamItem = _teamService.CreateTeam(teamForm, user.id);

        return CreatedAtAction(
            nameof(GetTeam),
            new { id = teamItem.id },
            new
            {
                teamItem.id,
                teamItem.name,
                teamItem.color
            });
    }

    /// <summary>
    /// Modify the <see cref="Team"/> corresponding to the id depending on the <see cref="TeamForm"/> informations
    /// </summary>
    /// <param name="id">Team id</param>
    /// <param name="teamForm">Infos to create a <see cref="Team"/></param>
    /// <returns>A <see cref="Team"/></returns>
    /// <response code="200">Returns the <see cref="Team"/> modified</response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    /// <response code="404">Team not found</response>
    [HttpPut("{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<Team>> ModifyTeam(Guid id, TeamForm teamForm)
    {
        var user = _userService.GetCurrentUser(HttpContext);

        try
        {
            var team = _teamService.ModifyTeam(id, teamForm, user.id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
    }


    /// <summary>
    /// Lists the <see cref="Team"/> which are involved in a specific <see cref="Sae"/>
    /// </summary>
    /// <param name="id">SAE id</param>
    /// <returns>A list of teams as <see cref="OutputGetTeamsBySaeId"/></returns>
    /// <response code="200">Returns teams as <see cref="OutputGetTeamsBySaeId"/> modified</response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Teacher access minimum]</response>
    /// <response code="404">SAE not found</response>
    [HttpGet("sae/{id}")]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public async Task<ActionResult<OutputGetTeamsBySaeId>> GetTeamsBySaeId(Guid id)
    {
        OutputGetTeamsBySaeId output = new() { teams = new() };

        var teams = _teamService.GetTeamsBySaeId(id);

        foreach (var team in teams)
        {
            var teamComposition = new TeamComposition
            {
                idTeam = team.id,
                nameTeam = team.name,
                colorTeam = team.color,
                idUsers = new List<Guid>()
            };

            List<User> users = _userService.GetUsersByTeamId(team.id);

            foreach (var user in users)
            {
                teamComposition.idUsers.Add(user.id);
            }

            output.teams.Add(teamComposition);
        }

        return output;
    }

    /// <summary>
    /// Make a for wish for a <see cref="Team"/> corresponding to the <see cref="WishForm"/> informations
    /// </summary>
    /// <param name="form">Infos for the wish</param>
    /// <returns>A list of wishes as <see cref="TeamWish"/></returns>
    /// <response code="200">Returns a list of wishes as <see cref="TeamWish"/></response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    [HttpPost("MakeWish")]
    [Authorize(Roles = RoleAccesses.Student)]
    public Task<ActionResult<List<TeamWish>>> MakeWish(WishForm form)
    {
        var user = _userService.GetCurrentUser(HttpContext);

        var teamWishList = _teamService.MakeWish(user.id, form.idTeam, form.idSubject);

        return Task.FromResult<ActionResult<List<TeamWish>>>(CreatedAtAction(
            nameof(MakeWish),
            new { id = form.idTeam },
            teamWishList));

    }
}
