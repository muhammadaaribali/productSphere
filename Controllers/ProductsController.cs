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
    public IActionResult CreateProduct([FromForm] CreateProductDto dto)
    {

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
        //dot.image,filename is the name of the uploaded file, and Path.GetExtension is used to get the file extension (e.g. .jpg, .png)
        //guid.NewGuid().ToString() is used to generate a unique filename for the uploaded file, which helps to avoid naming conflicts with other files

        var filePath = Path.Combine("wwwroot","images", fileName);
        //this points to wwwroot/images/3b8e27f4-a46d-43d6-95f5-c5d7d5a9d733.png

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            dto.Image.CopyTo(stream);
        }

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
            ImageUrl = "/images/" + fileName,
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

/*
A FileStream is a stream (a pipeline) that lets your program read from or write to a file.

Memory (uploaded image)
        │
        │
    FileStream
        │
        ▼
Hard Disk

(work flow)

Browser
   │
   ▼
Uploaded image
(cat.png)
   │
   ▼
dto.Image
   │
CopyTo(stream)
   │
   ▼
FileStream
   │
   ▼
Hard Disk
(wwwroot/images/cat.png)

*/