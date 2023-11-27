using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaeController : ControllerBase
    {
        public readonly ISaeService _saeService;

        public SaeController(ISaeService saeService)
        {
            _saeService = saeService;
        }

        [HttpPost]
        [Authorize(Roles = RoleAccesses.Teacher)]
        public ActionResult AddSae(SaeForm saeForm)
        {
            try
            {
                _saeService.CreateSae(saeForm);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public ActionResult<List<Sae>> GetSaesByUserId(Guid id)
        {
            var saes = _saeService.GetSaeByUserId(id);

            if (saes == null)
            {
                return NotFound();
            }

            return saes;
        }

        [HttpGet("admin/{id}")]
        [Authorize]
        public ActionResult<List<SaeAdminResponse>> GetSaesAdminByUserId(Guid id)
        {
            var saesNbGroups = _saeService.GetSaeAdminNbGroupByUserId(id);
            
            if (saesNbGroups == null)
            {
                return NotFound();
            }

            var saesNbCharacter = _saeService.GetSaeAdminNbStudentByUserId(id);

            if (saesNbCharacter == null)
            {
                return NotFound();
            }

            foreach (var sae in saesNbGroups)
            {
                var saesChar = saesNbCharacter.Find(s => s.id == sae.id);

                if (saesChar == null)
                {
                    sae.total_student = 0;
                }
                else
                {
                    sae.total_student = saesChar.total_student;
                }
            }

            return saesNbGroups;
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<SaeAdminResponse> GetSaes(Guid id)
        {
            var saesNbGroups = _saeService.GetSaeNbGroup(id);
            
            if (saesNbGroups == null)
            {
                return NotFound();
            }

            var saesNbCharacter = _saeService.GetSaeNbStudent(id);

            if (saesNbCharacter == null)
            {
                return NotFound();
            }

            saesNbGroups.total_student = saesNbCharacter.total_student;

            return saesNbGroups;
        }
    }
}
