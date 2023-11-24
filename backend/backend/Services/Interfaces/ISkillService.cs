using backend.Data.Models;

namespace backend.Services.Interfaces;

public interface ISkillService
{
    public List<Skill> GetSkills();
    public Skill GetSkillById(Guid id);
    public Skill AddSkill(string name);
    public void RemoveSkill(Guid id);

}