using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController: ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly EntityContext _context;

    public LoginController(IConfiguration configuration, EntityContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Login([FromBody] User userLogin)
    {
        var user = Authenticate(userLogin);
        Console.WriteLine(user);

        if (user != null)
        {
            var token = GenerateToken(user);
            return Ok(token);
        }

        return NotFound("user not found");
    }

    private User? Authenticate(User user)
    {
        return _context.Users.FirstOrDefault(x =>
            x.Username == user.Username && x.Password == user.Password);
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}