namespace sp1.Models;

public class Company
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required List<User> Users { get; set; }
}