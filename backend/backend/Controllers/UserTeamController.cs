using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserTeamController : ControllerBase
{
    private readonly IUserTeamService _userTeamService;

    public UserTeamController(IUserTeamService userTeamService)
    {
        _userTeamService = userTeamService;
    }

    /// <summary>
    /// Generates team depending on the infos contained on the <see cref="GenTeamForm"/>
    /// It will use an optimised algorithm to get the best combination of students capacities.
    /// Actual algorithm : Simulated Annealing
    /// </summary>
    /// <param name="genTeamForm">Infos for generating the teams</param>
    /// <returns>List of <see cref="UserTeam"/> : id_user, id_team and role of the user in the team</returns>
    /// <response code="200">Returns the calculated teams</response>
    /// <response code="401">Not authorized to access this method. [Teacher access minimum]</response>
    [HttpPost]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public ActionResult<List<UserTeam>> GenTeam(GenTeamForm genTeamForm)
    {
        var userTeams = _userTeamService.GenTeams(genTeamForm.id_users, genTeamForm.id_teams);

        return userTeams;
    }
}