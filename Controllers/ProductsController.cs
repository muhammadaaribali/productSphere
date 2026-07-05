using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
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
    [Authorize]
    public IActionResult CreateProduct(CreateProductDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            UserId = int.Parse(userId)
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return Ok("Product created successfully");
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _context.Products
            .OrderByDescending(p => p.Id)
            .ToList(); 
            //retrieve all products from the database, ordered by Id in descending order (newest first)
            //toList() is used to execute the query and return the results as a list of Product objects

        return Ok(products);
    }
}