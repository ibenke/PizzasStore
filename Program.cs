using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=DESKTOP-QLU2BFU\\SQLEXPRESS;initial Catalog=Pizzas;trusted_connection=True;TrustServerCertificate=True;Integrated Security=True";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlServer<PizzaDb>(connectionString);

builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "PizzaStore API",
         Description = "Making the Pizzas you love",
         Version = "v1" });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
   });
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/pizzas", async (PizzaDb db) =>
{
   var pizzas = await db.Pizzas
                        .Select(p => new
                        {
                           Id = p.Id,
                           Name = p.Name,
                           Description = p.Description,
                           ChefName = p.Chef.Name
                        })
                        .ToListAsync();
   return pizzas;
});
app.Run();