using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public string UserId => _contextAccessor.HttpContext.User.FindFirstValue("Id");
    public string Email => _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Email);
}