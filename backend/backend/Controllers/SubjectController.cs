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