using Microsoft.EntityFrameworkCore;
using Inventory.Application.Interfaces;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Controllers
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<InventoryDbDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<ILocationPointRepository, LocationPointRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
builder.Services.AddScoped<IInvoiceProductRepository, InvoiceProductRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IStockProductRepository, StockProductRepository>();

// Register MediatR (scans the assembly containing IUnitOfWork, i.e. Application layer)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IUnitOfWork).Assembly));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}