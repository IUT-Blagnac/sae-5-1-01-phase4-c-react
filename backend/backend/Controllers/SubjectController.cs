using backend.Data.Models;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    /// <summary>
    /// Lists the <see cref="Subject"/> of a specific <see cref="Sae"/>
    /// </summary>
    /// <param name="id">SAE id</param>
    /// <response code="200">Returns a list of <see cref="Subject"/></response>
    /// <response code="400">Database error or unknown exception</response>
    /// <response code="401">Not authorized to access this method. [Student access minimum]</response>
    /// <response code="404">Subjects not found</response>
    [HttpGet("sae/{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Subject>> GetSubjectBySaeId(Guid id)
    {
        var subjects = _subjectService.GetSubjectsBySaeId(id);

        if (subjects == null)
        {
            return NotFound();
        }

        return subjects;
    }
}