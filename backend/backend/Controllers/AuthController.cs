﻿using backend.Data.Models;
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

    /// <summary>
    /// Login a user with an email and a password
    /// </summary>
    /// <param name="userLogin"></param>
    /// <returns>A JWT token</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /login
    ///     {
    ///         "email": "test@test",
    ///         "password": "testtest"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Returns a JWT token</response>
    /// <response code="401">If the user is not found</response>
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

    /// <summary>
    /// Register a user with an email, a password, a first name, a last name and a group id (if the user is a student)
    /// </summary>
    /// <param name="userRegister"></param>
    /// <returns>The email and the first name of the new user created</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /register
    ///     {
    ///     "email": "test@test",
    ///     "password": "testtest",
    ///     "first_name": "test",
    ///     "last_name": "test",
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the email and the first name of the new user created</response>
    /// <response code="400">If there is database error</response>
    /// <response code="403">If the password is too short</response>
    /// <response code="409">If the user is already registered</response>
    /// <response code="500">If there is and internal error</response>
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

        return new OkObjectResult(new_users);
    }

    [HttpPost("sendcsv/{id_group}")]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public async Task<ActionResult> RegisterMultiplesFromCsv([FromForm] IFormFile file, Guid id_group)
    {
        List<UserCSVRegister> userRegisters;
        try
        {
            var enumerable = _csvService.ReadCSV<UserCSVRegister>(file.OpenReadStream());

            userRegisters = enumerable.ToList();
        }
        catch (Exception)
        {
            return StatusCode(422, new { message = "Invalid file or well not formated" });
        }

        List<UserCSVResponse> new_users;
        try
        {
            new_users = _userService.RegisterUsers(userRegisters, id_group);
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

        return new OkObjectResult(new_users);
    }

    #region Private Helpers

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