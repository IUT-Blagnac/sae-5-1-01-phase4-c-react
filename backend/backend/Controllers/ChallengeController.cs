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

    /// <summary>
    /// Get a challenge by its id
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <param name="id">The id of the challenge</param>
    /// <returns>The requested challenge</returns>
    /// <response code="200">Returns the requested challenge</response>
    /// <response code="401">If the user is not logged in</response>
    /// <response code="404">If the challenge is not found</response>
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

    /// <summary>
    /// Create a new challenge by giving a name, a description, a target team id and a creator team id
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <param name="challengeForm"></param>
    /// <returns>The challenge created</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /challenge
    ///     {
    ///     "name": "test",
    ///     "description": "test",
    ///     "target_team_id": "586e9ad9-13ad-407a-a709-9357ce294faa",
    ///     "creator_team_id": "e614f0b8-ef51-4c5d-b386-1934b77fe432",
    ///     }
    /// 
    /// </remarks>
    /// <response code="201">Returns the challenge created</response>
    /// <response code="401">If the user is not logged in</response>
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

    /// <summary>
    /// Set a challenge as completed by giving its id
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <param name="id">The id of the challenge to set completed</param>
    /// <returns>The challenge set as completed</returns>
    /// <response code="200">Returns the challenge set as completed</response>
    /// <response code="401">If the user is not logged in</response>
    /// <response code="404">If the challenge is not found</response>
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