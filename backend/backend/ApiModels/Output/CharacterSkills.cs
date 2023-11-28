using backend.Data.Models;

namespace backend.ApiModels.Output;

public class CharacterSkills
{
    public Guid id { get; set; }
    public string name { get; set; }
    public int confidence_level { get; set; }
}