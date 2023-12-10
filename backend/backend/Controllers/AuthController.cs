using backend.Data.Models;
using backend.FormModels;
using backend.Services.Class;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<UserLogin> _passwordHasher;
    private readonly ILogger<AuthController> _logger;

    private readonly IUserService _userService;
    private readonly ICSVService _csvService;
    private readonly IRoleUserService _roleUserService;

    public AuthController(IConfiguration configuration, ILogger<AuthController> logger, IUserService userService, ICSVService csvService, IRoleUserService roleUserService)
    {
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<UserLogin>();
        _logger = logger;
        _userService = userService;
        _csvService = csvService;
        _roleUserService = roleUserService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = Authenticate(userLogin);
        Console.WriteLine(user);

        if (user == null) return Unauthorized("user not found");

        var token = GenerateToken(user);
        return new OkObjectResult(new { Token = token });

    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult> Register([FromBody] UserRegister userRegister)
    {
        try
        {
            _userService.RegisterUser(email: userRegister.Email,
                                      passwd: userRegister.Password,
                                      first_name: userRegister.FirstName,
                                      last_name: userRegister.LastName,
                                      id_group: userRegister.id_group);
        }
        catch (UserService.RegisterException reg_ex)
        {
            return StatusCode(reg_ex.StatusCode, new { message = reg_ex.Message });
        }
        catch (DbException)
        {
            return StatusCode(400, new { message = "Database error" });
        }
        catch
        {
            return StatusCode(400, new { message = "Unknown exception" });
        }

        return Created("User created", new { Email = userRegister.Email, FirstName = userRegister.FirstName });
    }

    [HttpPost("register_multiple_json")]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public async Task<ActionResult> RegisterMultiplesFromJson([FromBody] List<UserRegister> userRegisters)
    {
        return RegisterMultiples(userRegisters);
    }

    [HttpPost("register_multiple_csv")]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public async Task<ActionResult> RegisterMultiplesFromCsv([FromForm] IFormFile file)
    {
        List<UserRegister> userRegisters;
        try
        {
            var enumerable = _csvService.ReadCSV<UserRegister>(file.OpenReadStream());

            userRegisters = enumerable.ToList();
        }
        catch (Exception)
        {
            return StatusCode(422, new { message = "Invalid file or well not formated" });
        }

        return RegisterMultiples(userRegisters);
    }

    #region Private Helpers

    private ActionResult RegisterMultiples(List<UserRegister> userRegisters)
    {
        List<User> new_users;
        try
        {
            new_users = _userService.RegisterUsers(userRegisters);
        }
        catch (UserService.RegisterException reg_ex)
        {
            return StatusCode(reg_ex.StatusCode, new { message = reg_ex.Message });
        }
        catch (DbException)
        {
            return StatusCode(400, new { message = "Database error" });
        }
        catch
        {
            return StatusCode(400, new { message = "Unknown exception" });
        }

        return Ok();
    }


    private User? Authenticate(UserLogin userLogin)
    {
        var user = _userService.GetUser(userLogin.Email);

        if (user is not null)
        {
            var passwordVerification = _passwordHasher.VerifyHashedPassword(userLogin, user.password, userLogin.Password);

            switch (passwordVerification)
            {
                case PasswordVerificationResult.Failed:
                    return null;
                case PasswordVerificationResult.Success:
                    return user;
            }

            _logger.LogWarning("Password hash algorithm is deprecated and should be changed");
        }

        return user;
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleuser = _roleUserService.GetRole(user.id_role);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Role, roleuser?.name ?? "Student")
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion Private Helpers
}