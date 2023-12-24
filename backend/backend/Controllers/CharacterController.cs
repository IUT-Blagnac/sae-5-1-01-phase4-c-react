using backend.ApiModels.Output;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;
    private readonly IUserService _userService;

    public CharacterController(ICharacterService characterService, IUserService userService)
    {
        _characterService = characterService;
        _userService = userService;
    }

    /// <summary>
    /// Create a character by giving a name and a sae id and a list of skills
    /// Note: Only students can access this route
    /// </summary>
    /// <param name="characterForm"></param>
    /// <returns>The character created</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /character
    ///     {
    ///     "name": "test",
    ///     "id_sae": "00000000-0000-0000-0000-000000000000",
    ///     "skills": [
    ///         {
    ///         "id_skill": "00000000-0000-0000-0000-000000000000",
    ///         "confidence_level": 1
    ///         },
    ///         {
    ///         "id_skill": "00000000-0000-0000-0000-000000000000",
    ///         "confidence_level": 2
    ///         }
    ///     ]
    ///     }
    /// 
    /// </remarks>
    /// <response code="201">Returns the character created</response>
    /// <response code="401">If the user is not a student</response>
    [HttpPost]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<Character> CreateCharacter(CharacterForm characterForm)
    {
        var user = _userService.GetCurrentUser(HttpContext);

        var character = _characterService.createCharacter(characterForm, user.id);

        return CreatedAtAction(
            nameof(GetCharacterById),
            new { id = character.id },
            new
            {
                id = character.id,
                name = character.name,
                id_user = character.id_user,
                id_sae = character.id_sae
            });
    }

    /// <summary>
    /// Get a character by its id
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <param name="id">The id of the requested character</param>
    /// <returns>The requested character</returns>
    /// <response code="200">Returns the requested character</response>
    /// <response code="401">If the user is not logged in</response>
    /// <response code="404">If the character is not found</response>
    [HttpGet("{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<Character> GetCharacterById(Guid id)
    {
        var character = _characterService.getCharacterById(id);

        if (character == null)
        {
            return NotFound();
        }

        return Ok();
    }

    /// <summary>
    /// Get the list of characters of a user
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <param name="id">The userId of the requested characters</param>
    /// <returns>The requested characters</returns>
    /// <response code="200">Returns the requested characters</response>
    /// <response code="401">If the user is not logged in</response>
    /// <response code="404">If the character is not found</response>
    [HttpGet("user/{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<CharacterSkills>> GetCharacterByUserId(Guid id)
    {
        var characters = _characterService.getCharacterByUserId(id);

        if (characters == null)
        {
            return NotFound();
        }

        return characters;
    }

    /// <summary>
    /// Get the list of characters of a sae
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <param name="id">The saeId of the requested characters</param>
    /// <returns>The requested characters</returns>
    /// <response code="200">Returns the requested characters</response>
    /// <response code="401">If the user is not logged in</response>
    /// <response code="404">If the character is not found</response>
    [HttpGet("sae/{id}")]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Character>> GetCharacterBySaeId(Guid id)
    {
        var characters = _characterService.getCharactersBySaeId(id);

        if (characters == null)
        {
            return NotFound();
        }

        return characters;
    }

    /// <summary>
    /// Get all characters
    /// Note: You need to be logged in to access this route
    /// </summary>
    /// <returns>A list with all the characters</returns>
    /// <response code="200">Returns a list with all the characters</response>
    /// <response code="401">If the user is not logged in</response>
    /// <response code="404">If the character is not found</response>
    [HttpGet]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Character>> GetCharacters()
    {
        return _characterService.getCharacters();
    }
}