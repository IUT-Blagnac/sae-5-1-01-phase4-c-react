﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        if (user == null) return Unauthorized("user not found");
        
        var token = GenerateToken(user);
        return new OkObjectResult(new {Token = token});

    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] UserRegister userRegister)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == userRegister.Email);

        if (user != null)
        {
            return StatusCode(409, new { message = "User already exits" });
        }

        if (userRegister.Password.Length < 8)
        {
            return StatusCode(409, new { message = "Password should be longer" });
        }

        var hashedPassword = _passwordHasher.HashPassword(userRegister,userRegister.Password);

        var userItem = new User
        {
            Id = Guid.NewGuid(),
            Email = userRegister.Email,
            Password = hashedPassword,
            FirstName = userRegister.FirstName,
            LastName = userRegister.LastName
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
        var user = _context.Users.FirstOrDefault(x => x.Email == userLogin.Email);

        if (user == null)
        {
            return null;
        }
        
        var passwordVerification = _passwordHasher.VerifyHashedPassword(userLogin, user.Password, userLogin.Password);

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
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}