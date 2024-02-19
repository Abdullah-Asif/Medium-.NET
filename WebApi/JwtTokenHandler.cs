using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Medium.Application.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace WebApi;

public static class JwtTokenHandler
{
    public static string CreateToken(UserDto userDto, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", userDto.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userDto.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userDto.Email),
                new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }
}