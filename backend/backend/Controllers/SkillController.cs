using backend.Data.Models;
using backend.Services.Interfaces;
using backend.Utils;
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

    /// <summary>
    /// Get all skills
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <returns>A list of all possible skills</returns>
    /// <response code="200">Returns a list of all possible skills</response>
    /// <response code="401">If the user is not logged in</response>
    [HttpGet]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Skill>> GetSkills()
    {
        return _skillService.GetSkills();
    }

    /// <summary>
    /// Get a skill by its id
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <param name="id">The id of the skill</param>
    /// <returns>The requested skill</returns>
    /// <response code="200">Returns the requested skill</response>
    /// <response code="401">If the user is not logged in</response>
    /// <response code="404">If the skill is not found</response>
    [HttpGet("{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public async Task<ActionResult<Skill>> GetSkillById(Guid id)
    {
        var skill = _skillService.GetSkillById(id);

        if (skill == null)
        {
            return NotFound();
        }

        return skill;
    }

    /// <summary>
    /// Create a new skill
    /// Note: Only teachers can access this route
    /// </summary>
    /// <param name="name">The name of the new skill</param>
    /// <returns>The created skill</returns>
    /// <response code="200">Returns the created skill</response>
    /// <response code="401">If the user is not a teacher</response>
    [HttpPost]
    [Authorize(Roles = RoleAccesses.Teacher)]
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

    /// <summary>
    /// Delete a skill by its id
    /// Note: Only teachers can access this route
    /// </summary>
    /// <param name="id">The id of the skill to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">If the skill is successfully deleted</response>
    /// <response code="401">If the user is not a teacher</response>
    /// <response code="404">If the skill is not found</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = RoleAccesses.Teacher)]
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