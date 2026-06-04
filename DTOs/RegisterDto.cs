namespace sp1.DTOs;

public class RegisterDto
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public int CompanyId { get; set; }
}