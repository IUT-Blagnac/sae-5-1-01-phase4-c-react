using backend.Data.Models;
using backend.FormModels;

namespace backend.Services.Interfaces;

public interface IChallengeservice
{
    public List<Challenge> GetChallenges();
    public Challenge GetChallengeById(Guid id);
    public List<Challenge> GetChallengesByCreatorTeamId(Guid team_id);
    public List<Challenge> GetChallengesByTargetTeamId(Guid team_id);
    public Challenge AddChallenge(ChallengeForm challengeForm);
    public Challenge ChallengeCompleted(Guid id);
}