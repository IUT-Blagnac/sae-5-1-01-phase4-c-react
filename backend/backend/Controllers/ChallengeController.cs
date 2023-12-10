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
public class ChallengeController : ControllerBase
{
    private readonly IChallengeservice _challengeservice;

    public ChallengeController(IChallengeservice challengeservice)
    {
        _challengeservice = challengeservice;
    }

    /// <summary>
    /// Get all challenges
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <returns>A list with all the challenges</returns>
    /// <response code="200">Returns a list with all the challenges</response>
    /// <response code="401">If the user is not logged in</response>
    [HttpGet]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Challenge>> GetChallenges()
    {
        return _challengeservice.GetChallenges();
    }

    /// <summary>
    /// Get the challenges created by a team
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <param name="id">The id of the team who create the challenges</param>
    /// <returns>A list of challenge created by the team</returns>
    /// <response code="200">Returns a list of challenge created by the team</response>
    /// <response code="401">If the user is not logged in</response>
    [HttpGet("creator/{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Challenge>> GetChallengesByCreatorTeamId(Guid id)
    {
        return _challengeservice.GetChallengesByCreatorTeamId(id);
    }
    
    /// <summary>
    /// Get the challenges where the team is the target
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <param name="id">The id of the team who is the target</param>
    /// <returns>A list of challenge where the team is the target</returns>
    /// <response code="200">Returns a list of challenge where the team is the target</response>
    /// <response code="401">If the user is not logged in</response>
    [HttpGet("target/{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Challenge>> GetChallengesByTargetTeamId(Guid id)
    {
        return _challengeservice.GetChallengesByTargetTeamId(id);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<Challenge>> GetChallengeById(Guid id)
    {
        var challenge = _challengeservice.GetChallengeById(id);

        if (challenge == null)
        {
            return NotFound();
        }

        return challenge;
    }

    [HttpPost]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<Challenge>> CreateChallenge(ChallengeForm challengeForm)
    {
        var challenge = _challengeservice.AddChallenge(challengeForm);

        return CreatedAtAction(
            nameof(GetChallengeById),
            new { id = challenge.id },
            new
            {
                id = challenge.id,
                name = challenge.name,
                description = challenge.description,
                completed = challenge.completed,
                tartget_team_id = challenge.target_team_id,
                creator_tema_id = challenge.creator_team_id
            });
    }

    [HttpGet("completed/{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<Challenge>> SetChallengeCompleted(Guid id)
    {
        try
        {
            var challenge = _challengeservice.ChallengeCompleted(id);

            if (challenge == null)
            {
                return NotFound();
            }

            return challenge;
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
    }
}