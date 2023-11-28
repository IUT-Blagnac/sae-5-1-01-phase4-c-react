using backend.ApiModels.Output;
using backend.Data.Models;
using backend.FormModels;

namespace backend.Services.Interfaces;

public interface ICharacterService
{
    public Character createCharacter(CharacterForm character, Guid userId);
    public List<CharacterSkills> getCharacterByUserId(Guid id);
    public List<Character> getCharacters();
    public List<Character> getCharactersBySaeId(Guid id);
    public Character getCharacterById(Guid id);
}