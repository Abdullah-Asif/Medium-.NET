using AutoMapper;
using Medium.Application.DTOs;
using Medium.Application.Interfaces;
using Medium.Domain.Entities;

namespace Medium.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Register(SignUpDto signUpDto)
    {
        await _userRepository.CreateAsync(_mapper.Map<User>(signUpDto));
    }

    public async Task<string> Login(SignInDto signInDto)
    {
        // var user = await _userRepository.GetAsync(x => x.Email == signInDto.Email);
        // if (user != null)
        // {
        //     
        // }
        throw new NotImplementedException();

    }

    public Task ResetPassword(string password)
    {
        throw new NotImplementedException();
    }

    public void RefreshToken()
    {
        throw new NotImplementedException();
    }
}