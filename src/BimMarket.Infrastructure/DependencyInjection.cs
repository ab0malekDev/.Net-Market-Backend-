using BimMarket.Application.Common.Abstractions;
using BimMarket.Infrastructure.Auth;
using BimMarket.Infrastructure.Data;
using BimMarket.Infrastructure.Identity;
using BimMarket.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BimMarket.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var conn = config.GetConnectionString("DefaultConnection") ?? "Host=localhost;Database=BimMarket;Username=postgres;Password=postgres";
        services.AddDbContext<AppDbContext>(o => o.UseNpgsql(conn));
        services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<IFlashDealRepository, FlashDealRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
