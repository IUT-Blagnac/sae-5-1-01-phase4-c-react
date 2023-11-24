using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Class;

public class ChallengeService: IChallengeservice
{
    private readonly EntityContext _context;

    public ChallengeService(EntityContext context)
    {
        _context = context;
    }

    public List<Challenge> GetChallenges()
    {
        return _context.Challenges.ToList();
    }

    public Challenge GetChallengeById(Guid id)
    {
        return _context.Challenges.FirstOrDefault(x => x.id == id);
    }

    public List<Challenge> GetChallengesByCreatorTeamId(Guid team_id)
    {
        return (from c in _context.Challenges
            where c.creator_team_id == team_id
            select new Challenge()
            {
                id = c.id,
                name = c.name,
                description = c.description,
                creator_team_id = c.creator_team_id,
                target_team_id = c.target_team_id,
                completed = c.completed
            }).ToList();
    }

    public List<Challenge> GetChallengesByTargetTeamId(Guid team_id)
    {
        return (from c in _context.Challenges
            where c.target_team_id == team_id
            select new Challenge()
            {
                id = c.id,
                name = c.name,
                description = c.description,
                creator_team_id = c.creator_team_id,
                target_team_id = c.target_team_id,
                completed = c.completed
            }).ToList();
    }

    public Challenge AddChallenge(ChallengeForm challengeForm, Guid creatorTeamId)
    {
        var challenge = new Challenge
        {
            id = Guid.NewGuid(),
            name = challengeForm.name,
            description = challengeForm.description,
            target_team_id = challengeForm.target_team_id,
            creator_team_id = creatorTeamId,
            completed = false
        };

        _context.Challenges.Add(challenge);
        _context.SaveChangesAsync();

        return challenge;
    }

    public Challenge ChallengeCompleted(Guid id)
    {
        var challenge = _context.Challenges.FirstOrDefault(x => x.id == id);

        challenge.completed = false;

        try
        {
            _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw e;
        }

        return challenge;
    }
}