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

        /// <summary>
        /// Create a new SAE by giving a name, a description, a list of group, a min and max number of student per group, a min and max number of group per subject, a list of coaches and a list of subjects
        /// Note: Only teachers can access this route
        /// </summary>
        /// <param name="saeForm"></param>
        /// <returns>The status code of the request</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /sae
        ///     {
        ///     "name": "test",
        ///     "description": "test",
        ///     "id_groups": [
        ///         "e614f0b8-ef51-4c5d-b386-1934b77fe432",
        ///         "bdfe25a9-c239-411d-a7f8-55d09f56fb98",
        ///         "8c060fe0-c52e-40ac-9020-f7f1e8608159"
        ///     ],
        ///     "min_student_per_group": 1,
        ///     "max_student_per_group": 2,
        ///     "min_group_per_subject": 1,
        ///     "max_group_per_subject": 2,
        ///     "id_coaches": [
        ///         "b8c86e1c-93f3-463a-8363-c1385ff12c6e",
        ///         "881b0034-f080-4b54-b66d-3f340155fe14"
        ///     ],
        ///     "subjects": [
        ///         {
        ///         name: "test",
        ///         description: "test",
        ///         categoriesId: [
        ///             "013d5857-c334-4cf4-aadd-68a51eeacd71",
        ///             "3c46e024-713e-4eea-8999-d69cee87e230"
        ///         ]
        ///         }
        ///     ]
        ///     }
        ///         
        /// </remarks>
        /// <response code="200">Returns the status code of the request</response>
        /// <response code="401">If the user is not a teacher</response>
        /// <response code="400">If the request is not valid or there is an error</response>
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

        /// <summary>
        /// Get all the SAE of a student by giving his user id
        /// Note: Only students can access this route
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The list of the SAE of the student</returns>
        /// <response code="200">Returns the list of the SAE of the student</response>
        /// <response code="401">If the user is not a student</response>
        /// <response code="404">If the SAE is not found</response>
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

        /// <summary>
        /// Get all the SAE
        /// Note: Only admins can access this route
        /// </summary>
        /// <returns>A list of all the SAE</returns>
        /// <response code="200">Returns a list of all the SAE</response>
        /// <response code="401">If the user is not an admin</response>
        [HttpGet("admin")]
        [Authorize(Roles = RoleAccesses.Admin)]
        public ActionResult<List<SaeAdminResponse>> GetSaesAdmin()
        {
            return _saeService.GetSaes();
        }

        /// <summary>
        /// Get all the SAE of a teacher by giving his user id
        /// Note: Only admins can access this route
        /// </summary>
        /// <param name="user_id">The id of the teacher</param>
        /// <returns>The list of SAE of the teacher</returns>
        /// <response code="200">Returns the list of SAE of the teacher</response>
        /// <response code="401">If the user is not an admin</response>
        /// <response code="404">If the SAE is not found</response>
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
                    sae.total_nb_student = 0;
                }
                else
                {
                    sae.total_nb_student = saesChar.total_nb_student;
                }
            }

            return saesNbGroups;
        }

        /// <summary>
        /// Get a SAE by giving its id
        /// Note: You need to be logged in to access this route
        /// </summary>
        /// <param name="id">The id of the requested SAE</param>
        /// <returns>The requested SAE</returns>
        /// <response code="200">Returns the requested SAE</response>
        /// <response code="401">If the user is not logged in</response>
        /// <response code="404">If the SAE is not found</response>
        [HttpGet("{id}")]
        [Authorize(Roles = RoleAccesses.Student)]
        public ActionResult<SaeAdminResponse> GetSae(Guid id)
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

            saesNbGroups.total_nb_student = saesNbCharacter.total_nb_student;

            return saesNbGroups;
        }
        
        /// <summary>
        /// Change SAE state to the given state by giving its id and the state
        /// Note: Only teachers can access this route
        /// </summary>
        /// <param name="id">The id of the SAE to change state</param>
        /// <param name="state">The value of the new state as int</param>
        /// <returns>The requested SAE</returns>
        /// <response code="200">Returns the requested SAE</response>
        /// <response code="401">If the user is not a teacher</response>
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
                case State.LAUNCHED:
                    return _saeService.SetSaeToLaunched(id);
                case State.LAUNCHED_OPEN_FOR_INTERNSHIP:
                    //"not implemented";
                    break;
                case State.CLOSED:
                    return _saeService.SetSaeToClosed(id);
            }


            return null;
        }

        /// <summary>
        /// Get all the teams of a SAE by giving its id
        /// </summary>
        /// <param name="id">The SAE id</param>
        /// <returns>A list of all the teams of the requested SAE</returns>
        /// <response code="200">Returns a list of all the teams of the requested SAE</response>
        /// <response code="401">If the user is not a teacher</response>
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
