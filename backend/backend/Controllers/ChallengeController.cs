using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChallengeController: ControllerBase
{
    private readonly IChallengeservice _challengeservice;

    public ChallengeController(IChallengeservice challengeservice)
    {
        _challengeservice = challengeservice;
    }

    [HttpGet]
    [Authorize]
    public ActionResult<List<Challenge>> GetChallenges()
    {
        return _challengeservice.GetChallenges();
    }

    [HttpGet("/creator/{id}")]
    [Authorize]
    public ActionResult<List<Challenge>> GetChallengesByCreatorTeamId(Guid id)
    {
        return _challengeservice.GetChallengesByCreatorTeamId(id);
    }

    [HttpGet("/target/{id}")]
    [Authorize]
    public ActionResult<List<Challenge>> GetChallengesByTargetTeamId(Guid id)
    {
        return _challengeservice.GetChallengesByTargetTeamId(id);
    }

    [HttpGet("{id}")]
    [Authorize]
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
    [Authorize]
    public async Task<ActionResult<Challenge>> CreateChallenge(ChallengeForm challengeForm)
    {
        
    }
}