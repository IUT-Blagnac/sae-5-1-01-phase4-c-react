namespace backend.ApiModels.Output;

public class CharacterByUserId
{
    public Guid id { get; set; }
    public string name { get; set; }
    public Guid id_user { get; set; }
    public Guid id_sae { get; set; }
    public List<CharacterSkills> skills { get; set; }
}