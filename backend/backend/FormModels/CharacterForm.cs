namespace backend.FormModels;

public class CharacterForm
{
    public string name { get; set; }
    public Guid id_sae { get; set; }
    public List<CharacterSkillForm> skills { get; set; }
}