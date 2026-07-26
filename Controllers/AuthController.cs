using Microsoft.AspNetCore.Mvc;
using sp1.Data;
using sp1.DTOs;
using sp1.Models;
using Microsoft.IdentityModel.Tokens;
//import the necessary namespaces for JWT token generation and claims handling like symmetric security keys and signing credentials
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
//import the necessary namespaces for JWT token generation and claims handling
using System.Security.Cryptography;

using System.Text;

namespace sp1.Controllers;

[ApiController] 
//indicates that this class is an API controller and enables features like automatic model validation and binding source inference
[Route("api/[controller]")]
public class AuthController : ControllerBase
//create a new controller called AuthController that inherits from ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context,IConfiguration configuration)
    {
        _context = context;
        //create a constructor that takes in an AppDbContext and assigns it to a private field
        _configuration = configuration;
    }

    private string GenerateJwtToken(User user)
{
    //array of claims objects that will be included in the JWT token. In this case, we are including the user's ID and email as claims.
    var claims = new[]
    {
        new Claim(
            ClaimTypes.NameIdentifier,
            user.Id.ToString()
        ),

        new Claim(
            ClaimTypes.Email,
            user.Email
        )
    };

    //create a new instance of SymmetricSecurityKey using the secret key from the appsettings.json file. The key is converted to a byte array using UTF8 encoding.
    
    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"]!
        )
    );
    
    //credentials object that will be used to sign the JWT token. In this case, we are using the HMAC SHA256 algorithm to sign the token with the symmetric security key.

    var credentials = new SigningCredentials(
        key,
        SecurityAlgorithms.HmacSha256
    );

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler()
        .WriteToken(token);
        
        //returns the generated JWT token as a string by using the JwtSecurityTokenHandler class to write the token to a string format.
}
    private string GenerateRefreshToken()
{
    var randomBytes = RandomNumberGenerator.GetBytes(64);

    return Convert.ToBase64String(randomBytes);
}

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    //create a new endpoint for user registration that accepts a RegisterDto object as input
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            CompanyId = dto.CompanyId
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

        /*SELECT *
        FROM "Users"
        WHERE "Email" = 'aarib@gmail.com'
        LIMIT 1;
        */

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        bool isPasswordValid =
            BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            return Unauthorized("Invalid email or password");
        }

        var token = GenerateJwtToken(user);
        return Ok(new
        {
            token = token
        });
    }
}