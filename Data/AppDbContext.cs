using Microsoft.EntityFrameworkCore; //import the Entity Framework Core library

using sp1.Models; //import the User model from the Models folder

namespace sp1.Data;

public class AppDbContext : DbContext 
//create a new class called AppDbContext that inherits from DbContext

 //options are hostname, port, database name, username, and password for the PostgreSQL database

{
    public AppDbContext 
    (DbContextOptions<AppDbContext> options) : base(options) { } 
    
    //create a constructor that takes in DbContextOptions and passes it to the base class constructor

    public DbSet<User> Users { get; set; } 
    
    //create a DbSet property for the User model, which will allow us to interact with the Users table in the database

    public DbSet<Company> Companies { get; set; }

    public DbSet<Product> Products { get; set; }
}