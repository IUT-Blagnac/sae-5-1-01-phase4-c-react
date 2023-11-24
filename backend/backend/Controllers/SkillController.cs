using backend.Data.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillController: ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    [Authorize]
    public ActionResult<List<Skill>> GetSkills()
    {
        return _skillService.GetSkills();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Skill>> GetSkillById(Guid id)
    {
        var skill = _skillService.GetSkillById(id);

        if (skill == null)
        {
            return NotFound();
        }

        return skill;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Skill>> AddSkill(string name)
    {
        var skill = _skillService.AddSkill(name);

        return CreatedAtAction(
            nameof(GetSkillById),
            new { id = skill.id },
            new
            {
                skill.id,
                skill.name
            });
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> RemoveSkill(Guid id)
    {
        var skill = _skillService.GetSkillById(id);

        if (skill == null)
        {
            return NotFound();
        }
        
        _skillService.RemoveSkill(id);

        return NoContent();
    }
}