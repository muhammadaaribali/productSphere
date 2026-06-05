using Microsoft.AspNetCore.Mvc;
using sp1.Data;
using sp1.DTOs;
using sp1.Models;

namespace sp1.Controllers;

[ApiController] 
//indicates that this class is an API controller and enables features like automatic model validation and binding source inference
[Route("api/[controller]")]
public class AuthController : ControllerBase
//create a new controller called AuthController that inherits from ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
        //create a constructor that takes in an AppDbContext and assigns it to a private field
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

        return Ok("Login successful");
    }
}