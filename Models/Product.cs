namespace sp1.Models;

public class Product
{
    public int Id { get; set;}
    public required string Name { get; set;}
    public required string Description { get; set;}
    public decimal Price { get; set;}
    public required string ImageUrl { get; set;}
    public int UserId { get; set;}
    public User? User { get; set;} //navigation property to the User who created the product

    public DateTime CreatedAt { get; set;} = DateTime.UtcNow;
}