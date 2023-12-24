using backend.Data;
using backend.Data.Models;
using backend.Services.Interfaces;

namespace backend.Services.Class;

public class SkillService: ISkillService
{
    private readonly EntityContext _context;

    public SkillService(EntityContext context)
    {
        _context = context;
    }
    
    public List<Skill> GetSkills()
    {
        return _context.Skills.ToList();
    }

    public Skill GetSkillById(Guid id)
    {
        return _context.Skills.FirstOrDefault(x => x.id == id);
    }

    public Skill AddSkill(string name)
    {
        var skill = new Skill
        {
            id = Guid.NewGuid(),
            name = name
        };
        
        _context.Skills.Add(skill);
        _context.SaveChangesAsync();

        return skill;
    }

    public void RemoveSkill(Guid id)
    {
        var skill = _context.Skills.FirstOrDefault(x => x.id == id);

        _context.Remove(skill);
        _context.SaveChangesAsync();
    }
}