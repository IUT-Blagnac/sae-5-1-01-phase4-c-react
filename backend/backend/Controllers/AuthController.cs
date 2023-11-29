using backend.Data;
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
    private readonly EntityContext _context;
    private readonly PasswordHasher<UserLogin> _passwordHasher;
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;

    public AuthController(IConfiguration configuration, EntityContext context, ILogger<AuthController> logger, IUserService userService)
    {
        _configuration = configuration;
        _context = context;
        _passwordHasher = new PasswordHasher<UserLogin>();
        _logger = logger;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public ActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = Authenticate(userLogin);
        Console.WriteLine(user);

        if (user == null) return Unauthorized("user not found");

        var token = GenerateToken(user);
        return new OkObjectResult(new { Token = token });

    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] UserRegister userRegister)
    {
        try
        {
            _userService.RegisterUser(email: userRegister.Email,
                                      passwd: userRegister.Password,
                                      first_name: userRegister.FirstName,
                                      last_name: userRegister.LastName);
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

    [AllowAnonymous]
    [HttpPost]
    [Route("register_multiple")]
    public async Task<ActionResult> RegisterMultiples([FromBody] List<UserRegister> userRegisters)
    {
        try
        {
            foreach (UserRegister userRegister in userRegisters)
            {
                _userService.RegisterUser(email: userRegister.Email,
                                          passwd: userRegister.Password,
                                          first_name: userRegister.FirstName,
                                          last_name: userRegister.LastName);
            }
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

        return StatusCode(201, "Users created");
    }

    private User? Authenticate(UserLogin userLogin)
    {
        var user = _context.Users.FirstOrDefault(x => x.email == userLogin.Email);

        if (user == null)
        {
            return null;
        }
        var passwordVerification = _passwordHasher.VerifyHashedPassword(userLogin, user.password, userLogin.Password);

        switch (passwordVerification)
        {
            case PasswordVerificationResult.Failed:
                return null;
            case PasswordVerificationResult.Success:
                return user;
        }

        _logger.LogWarning("Password hash algorithm is deprecated and should be changed");
        return user;
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleuser = _context.Roles.First(x => x.id == user.id_role);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Role, roleuser.name)
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public async Task<ActionResult<User>> AddUsers(List<UserRegister> userRegisters)
    {
        foreach (var userRegister in userRegisters)
        {
            Register(userRegister);
        }
        var new_user = _userService.AddUser(mail, passwd, name, surname);

        return CreatedAtAction(
            nameof(GetAuthenticatedUser),
            new { id = new_user.id },
            new
            {
                new_user.id,
                new_user.email,
                new_user.password,
                new_user.first_name,
                new_user.last_name
                //new_user.id_role,
                //new_user.id_group
            });
    }
}