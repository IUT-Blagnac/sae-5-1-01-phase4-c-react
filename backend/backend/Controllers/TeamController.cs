using System.Collections;
using System.Security.Claims;
using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Class;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController: ControllerBase
{
    private readonly EntityContext _context;
    private readonly ITeamService _teamService;

    public TeamController(EntityContext context, ITeamService teamService)
    {
        _context = context;
        _teamService = teamService;
    }

    [HttpGet]
    [Authorize]
    public ActionResult<List<Team>> GetTeams()
    {
        var user = GetCurrentUser();

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

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Team>> CreateTeam(TeamForm teamForm)
    {
        var user = GetCurrentUser();

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
        var user = GetCurrentUser();
        
        try
        {
            var team = _teamService.MoifyTeam(id, teamForm, user.id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        } catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
    }
    
    private User? GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            
            return _context.Users.FirstOrDefault(x => x.email == email);
        }

        return null;
    }
}