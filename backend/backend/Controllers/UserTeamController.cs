using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
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
    
    [HttpPost]
    public ActionResult<List<UserTeam>> GenTeam(GenTeamForm genTeamForm)
    {
        var userTeams = _userTeamService.GenTeams(genTeamForm.id_users, genTeamForm.id_teams);

        return userTeams;
    } 
}