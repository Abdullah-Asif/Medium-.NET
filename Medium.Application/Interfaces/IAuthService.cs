using Medium.Application.DTOs;

namespace Medium.Application.Interfaces;

public interface IAuthService
{
    Task Register(SignUpDto signUpDto);
    Task<string> Login(SignInDto signInDto);
    Task ResetPassword(string password);
    
    // send new access token until the existing refresh token is not expired
    // otherwise send invalid token response in return so that user must have to logout and sign in again
    void RefreshToken();  
}