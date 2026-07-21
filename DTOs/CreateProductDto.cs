using Microsoft.AspNetCore.Http;
namespace sp1.DTOs;

public class CreateProductDto
{
    public required string Name { get; set;}
    public required string Description { get; set;}
    public decimal Price { get; set;}
    //public required string ImageUrl { get; set;}

    public IFormFile? Image { get; set; }
    //property to hold the uploaded image file
}