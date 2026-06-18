using Microsoft.EntityFrameworkCore;
using sp1.Data;//import the AppDbContext class from the Data folder

var builder = WebApplication.CreateBuilder(args);
//create a new WebApplicationBuilder instance, which will be used to configure the application

builder.Services.AddControllers(); 
//add support for controllers, which will handle incoming HTTP requests and return responses

builder.Services.AddEndpointsApiExplorer();
//add support for API endpoints, which will allow us to define routes for our controllers and generate API documentation
builder.Services.AddSwaggerGen();
//add support for Swagger, which will generate API documentation and provide a user interface for testing our API endpoints


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        //get the connection string from the appsettings.json file and use it to configure the DbContext to use PostgreSQL
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();
//build the application using the configured services and middleware

app.UseCors("ReactPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //if the application is running in development mode, enable Swagger and the Swagger UI for testing the API endpoints
}

// app.UseHttpsRedirection();

app.MapControllers();
//map the controllers to the application's request pipeline, which will allow them to handle incoming HTTP requests

app.Run();