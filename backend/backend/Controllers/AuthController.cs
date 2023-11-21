using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly EntityContext _context;
    private readonly PasswordHasher<UserLogin> _passwordHasher;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration configuration, EntityContext context, ILogger<AuthController> logger)
    {
        _configuration = configuration;
        _context = context;
        _passwordHasher = new PasswordHasher<UserLogin>();
        _logger = logger;
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
        return new OkObjectResult(new {Token = token});

    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] UserRegister userRegister)
    {
        var user = _context.Users.FirstOrDefault(x => x.email == userRegister.Email);

        if (user != null)
        {
            return StatusCode(409, new { message = "User already exits" });
        }

        var defaultRole = _context.RoleUsers.Where(c => c.name == "Student").FirstOrDefault();
        if (defaultRole == null)
        {
            return StatusCode(500, new { Message = "Internal server error" });
        }

        var hashedPassword = _passwordHasher.HashPassword(userRegister,userRegister.Password);

        var userItem = new User
        {
            id = Guid.NewGuid(),
            email = userRegister.Email,
            password = hashedPassword,
            first_name = userRegister.FirstName,
            last_name = userRegister.LastName,
            role_user = defaultRole
        };

        _context.Users.Add(userItem);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new { Message = "Internal server error" });
        }

        return Created("User created", new { Email = userRegister.Email, FirstName = userRegister.FirstName });
    }

    private User? Authenticate(UserLogin userLogin)
    {
        var user = _context.Users.FirstOrDefault(x => x.email == userLogin.Email);

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

        var role = _context.RoleUsers.FirstOrDefault(x => x.id == user.role_id);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Role, role.name)
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}