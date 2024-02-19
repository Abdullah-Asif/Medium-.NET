using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.Services;

namespace WebApi;

public static class ConfigureServices
{   
    public static IServiceCollection RegisterWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"), 
                b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)).EnableSensitiveDataLogging());
        services.AddSwaggerGen();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromSeconds(0)
            };
        });
        services.AddAuthorization();
        return services;
    }
}