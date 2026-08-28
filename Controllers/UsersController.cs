using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using sp1.Data;
using sp1.DTOs;

namespace sp1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context=context;
    }

    [HttpGet("profile")]
    [Authorize]
    public IActionResult GetProfile()
    {
        var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }


        var user = _context.Users.FirstOrDefault(
            u => u.Id== int.Parse(userId)
        );

        if( user== null)
        {
            return NotFound("User not found");
        }

        return Ok(new
        {
            id = user.Id,
            name= user.Name,
            email= user.Email
        });
    }

    [HttpPut("profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        var user= await _context.Users.FindAsync(int.Parse(userId));

        if(user == null)
        {
            return NotFound("User not found");
        }

        user.Name=dto.Name;

        await _context.SaveChangesAsync();

        return Ok( new
        {
            message= "Profile updated successfully",
            name=user.Name
        }); 
    }
}