using System.Collections;
using System.Security.Claims;
using backend.ApiModels.Output;
using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Class;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
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

    [HttpGet]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Team>> GetTeams()
    {
        var user = _userService.GetCurrentUser(HttpContext);

        return _teamService.GetTeams(user.id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Team>> GetTeam(Guid id)
    {
        var team = _teamService.GetTeam(id);

        if (team == null)
        {
            return NotFound();
        }

        return team;
    }

    [HttpGet]
    [Route("{user_id}/{sae_id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public Team? GetTeamByUserIdAndSaeId(Guid user_id, Guid sae_id)
    {
        return _teamService.GetTeamByUserIdAndSaeId(user_id, sae_id);
    }

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

    [HttpPut("{id}")]
    [Authorize]
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

    [HttpPost("MakeWish")]
    [Authorize(Roles = RoleAccesses.Student)]
    public Task<ActionResult<List<TeamWish>>> MakeWish(WishForm form)
    {
        var user = _userService.GetCurrentUser(HttpContext);
        
        var teamWishList = _teamService.MakeWish(user.id,form.idTeam, form.idSubject);

        return Task.FromResult<ActionResult<List<TeamWish>>>(CreatedAtAction(
            nameof(MakeWish),
            new { id = form.idTeam },
            teamWishList));

    }
}
