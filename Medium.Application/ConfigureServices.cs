using Medium.Application.Interfaces;
using Medium.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.Application;

public static class ConfigureServices
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBlogService, BlogService>();
        return services;
    }
}