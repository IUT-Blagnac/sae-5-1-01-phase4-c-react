using backend.ApiModels.Output;
using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using CharacterSkill = backend.Data.Models.CharacterSkill;

namespace backend.Services.Class;

public class CharacterService: ICharacterService
{
    private readonly EntityContext _context;

    public CharacterService(EntityContext context)
    {
        _context = context;
    }
    
    public Character createCharacter(CharacterForm characterForm, Guid userId)
    {
        var character = _context.Characters.FirstOrDefault(c => c.id_user == userId);
        var res = new Character();
        
        if (character == null)
        {
            Guid idCharacter = Guid.NewGuid();

            Character newCharacter = new()
            {
                id = idCharacter,
                name = characterForm.name,
                id_user = userId,
                id_sae = characterForm.id_sae
            };

            _context.Characters.Add(newCharacter);
            _context.SaveChanges();

            res = newCharacter;
        }
        else
        {
            var skills = _context.CharacterSkills.ToList();

            if (skills != null)
            {
                foreach (var s in skills)
                {
                    _context.Remove(s);
                }

                _context.SaveChanges();
            }

            res = character;
        }

        foreach (CharacterSkillForm cs in characterForm.skills)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.id == cs.id_skill);

            if (skill == null)
            {
                return null;
            }
            
            _context.CharacterSkills.Add(new CharacterSkill
            {
                id_character = character.id,
                id_skill = cs.id_skill,
                confidence_level = cs.confidence_level
            });
        }

        _context.SaveChanges();
        
        return res;
    }

    public List<CharacterSkills> getCharacterByUserId(Guid id)
    {
        var query = (from u in _context.Users
            join c in _context.Characters on u.id equals c.id_user
            join cs in _context.CharacterSkills on c.id equals cs.id_character
            join s in _context.Skills on cs.id_skill equals s.id
            where u.id == id
            select new CharacterSkills()
            {
                id = s.id,
                name = s.name,
                confidence_level = cs.confidence_level,
            }).ToList();

        return query;
    }

    public List<Character> getCharacters()
    {
        return _context.Characters.ToList();
    }

    public List<Character> getCharactersBySaeId(Guid id)
    {
        return (from c in _context.Characters
            where c.id_sae == id
            select c).ToList();
    }

    public Character getCharacterById(Guid id)
    {
        return _context.Characters.FirstOrDefault(c => c.id == id);
    }
}