using backend.ApiModels.Output;
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
        private readonly ISaeService _saeService;
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;

        public SaeController(ISaeService saeService, ITeamService teamService, IUserService userService)
        {
            _saeService = saeService;
            _teamService = teamService;
            _userService = userService;
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
        [Authorize(Roles = RoleAccesses.Student)]
        public ActionResult<List<Sae>> GetSaesByUserId(Guid id)
        {
            var saes = _saeService.GetSaeByUserId(id);

            if (saes == null)
            {
                return NotFound();
            }

            return saes;
        }

        [HttpGet("admin")]
        [Authorize(Roles = RoleAccesses.Admin)]
        public ActionResult<List<SaeAdminResponse>> GetSaesAdmin()
        {
            return _saeService.GetSaes();
        }

        [HttpGet("admin/{user_id}")]
        [Authorize(Roles = RoleAccesses.Admin)]
        public ActionResult<List<SaeAdminResponse>> GetSaesAdminByUserId(Guid user_id)
        {
            var saesNbGroups = _saeService.GetSaeAdminNbGroupByUserId(user_id);

            if (saesNbGroups == null)
            {
                return NotFound();
            }

            var saesNbCharacter = _saeService.GetSaeAdminNbStudentByUserId(user_id);

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

        /**
         * Méthode pour passer d'un état à un autre
         *
         * Implémenté pour l'instant
         * 
         * Bascule d'une SAE de pending users à pending wished
            
            IN
            {
            "sae_id": "la sae cible"
            }
            
            
            OUT
            // Passage de la SAE de PENDING_USERS à PENDING_WISHES
            // Génération des groupes en parallèle
            
            
         */
        [HttpGet("passToState/{id}/{state}")]
        [Authorize(Roles = RoleAccesses.Teacher)]
        public ActionResult<SaeAdminResponse> PassToState(Guid id, State state)
        {

            switch (state)
            {
                case State.PENDING_USERS:
                    //not implemented, normaly shouldn't be used
                    break;
                case State.PENDING_WISHES:
                    return _saeService.SetSaeToPendingWishes(id);
                    break;
                case State.LAUNCHED:
                    //"not implemented";
                    break;
                case State.LAUNCHED_OPEN_FOR_INTERNSHIP:
                    //"not implemented";
                    break;
                case State.CLOSED:
                    //"not implemented";
                    break;
            }


            return null;
        }

        [HttpGet("teams/{id}")]
        [Authorize(Roles = RoleAccesses.Admin)]
        public async Task<ActionResult<OutputGetTeamsBySaeId>> GetTeamsBySaeId(Guid id)
        {
            OutputGetTeamsBySaeId output = new() { teams = new() };

            var teams = _teamService.GetTeamsBySaeId(id);

            foreach (var team in teams)
            {
                var teamComposition = new TeamComposition
                {
                    idTeam = team.id,
                    nameTeam = team.name,
                    colorTeam = team.color,
                    idUsers = new List<Guid>()
                };

                List<User> users = _userService.GetUsersByTeamId(team.id);

                foreach (var user in users)
                {
                    teamComposition.idUsers.Add(user.id);
                }

                output.teams.Add(teamComposition);
            }

            return output;
        }
    }
}
