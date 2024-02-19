using Medium.Application.Interfaces;
using Medium.Infrastructure.Repositories;
using Medium.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        return services;
    }
}