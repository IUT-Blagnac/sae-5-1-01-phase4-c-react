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

    [HttpGet]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Challenge>> GetChallenges()
    {
        return _challengeservice.GetChallenges();
    }

    [HttpGet("creator/{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Challenge>> GetChallengesByCreatorTeamId(Guid id)
    {
        return _challengeservice.GetChallengesByCreatorTeamId(id);
    }

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