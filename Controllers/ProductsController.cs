using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
        if(dto.Image == null || dto.Image.Length == 0)
        {
            return BadRequest("Image is required");
        }

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
        var products = _context.Products.Include(p=> p.User).OrderByDescending(p=>p.Id).Select(p=> new ProductResponseDto
            {
                Id=p.Id,
                Name=p.Name,
                Description=p.Description,
                Price=p.Price,
                ImageUrl=p.ImageUrl,
                UploadedBy = p.User != null ? p.User.Name : "Unknown"
            })
            .ToList(); 
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



React
   │
   │ api.get("/Products")
   ▼
ProductsController.GetProducts()
   │
   ▼
_context.Products
   │
   ▼
Include(User)
   │
   ▼
Load each product's uploader
   │
   ▼
Order newest first
   │
   ▼
Convert Product → ProductResponseDto
   │
   ▼
Execute query with ToList()
   │
   ▼
Return HTTP 200 OK + JSON
   │
   ▼
React receives the product list
   │
   ▼
Displays:
Product Name
Description
Price
Image
Uploaded by "name"
*/