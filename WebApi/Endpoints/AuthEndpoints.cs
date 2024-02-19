using AutoMapper;
using Medium.Application.DTOs;
using Medium.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.Endpoints;

public static class AuthEndpoints
{
    public static void RegisterAuthEndpoint(this WebApplication app)
    {
        var authGroup = app.MapGroup("api/auth");
        authGroup.MapPost("sign-up", Registration);
        authGroup.MapPost("sign-in", Login);
    }

    private static async Task<Results<Ok<string>, NotFound, BadRequest<string>>> Login([FromBody]SignInDto signInDto, IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        try
        {
            var user = await userRepository.GetAsync(x => x.Email == signInDto.Email);
            if (user == null)
            {
                return TypedResults.NotFound();
            }

            if (user.Password != signInDto.Password)
            {
                return TypedResults.BadRequest("Incorrect password");
            }
            var token = JwtTokenHandler.CreateToken(mapper.Map<UserDto>(user), configuration);
            return TypedResults.Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static async Task<Ok<string>> Registration(IAuthService authService, SignUpDto signUpDto)
    { 
        await authService.Register(signUpDto);
        return TypedResults.Ok("Registration successful");
    }
}