using Microsoft.AspNetCore.Mvc;
using sp1.Data;
using sp1.DTOs;
using sp1.Models;

namespace sp1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateProduct(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            UserId = dto.UserId
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return Ok("Product created successfully");
    }
}