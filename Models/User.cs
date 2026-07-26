namespace sp1.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public int CompanyId { get; set; }

    public  Company? Company { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set;} = new List<RefreshToken>();
    //ICollection<RefreshToken> property is used to represent the one-to-many relationship between the User and RefreshToken entities. It allows a user to have multiple refresh tokens associated with their account.

}