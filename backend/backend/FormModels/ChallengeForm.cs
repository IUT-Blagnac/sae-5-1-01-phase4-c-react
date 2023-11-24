namespace backend.FormModels;

public class ChallengeForm
{
    public string name { get; set; }
    public string description { get; set; }
    public Guid target_team_id { get; set; }
}