Application starts
       |
       v
AddDbContext<AppDbContext>()
(registers how to create AppDbContext)
       |
       v
POST /api/Products arrives
       |
       v
ASP.NET creates AppDbContext
       |
       v
AppDbContext constructor runs
       |
       v
ASP.NET creates ProductsController
       |
       v
ProductsController constructor runs
       |
       v
_context = context
       |
       v
CreateProduct() executes