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

    [HttpGet]
    [Authorize(Roles = RoleAccesses.Student)]
    public ActionResult<List<Character>> GetCharacters()
    {
        return _characterService.getCharacters();
    }
}