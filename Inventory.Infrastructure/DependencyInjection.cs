using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Persistence.Repositories;
using Inventory.Infrastructure.Services;

namespace Inventory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<InventoryDbDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Register UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Repositories
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<ILocationPointRepository, LocationPointRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
        services.AddScoped<IInvoiceProductRepository, InvoiceProductRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IStockProductRepository, StockProductRepository>();

        // Register Services
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<IClientsService, ClientsService>();
        services.AddScoped<IInvoicesService, InvoicesService>();
        services.AddScoped<ILocationPointsService, LocationPointsService>();
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<IPurchasesService, PurchasesService>();
        services.AddScoped<IVendorsService, VendorsService>();

        // Register MediatR (scans the assembly containing IUnitOfWork, i.e. Application layer)
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IUnitOfWork).Assembly));

        return services;
    }
}
