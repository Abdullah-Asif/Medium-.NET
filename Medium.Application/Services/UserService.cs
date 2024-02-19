using AutoMapper;
using Medium.Application.DTOs;
using Medium.Application.Exceptions;
using Medium.Application.Interfaces;
using Medium.Application.Utilities;
using Medium.Domain.Entities;

namespace Medium.Application.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAllUsers(PaginationQueryRequest paginationQueryRequest)
    {
        var users = await _userRepository.GetAllAsync(paginationQueryRequest);
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task CreateUser(SignUpDto signUpDto)
    {
        await _userRepository.CreateAsync(_mapper.Map<User>(signUpDto));
    }

    public async Task<UserDto> GetUserById(string id)
    {
        var user = await _userRepository.GetAsync(x => x.Id.ToString() == id, false);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByName(string username)
    {
        var user = await _userRepository.GetAsync(x => x.UserName == username);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetAsync(x => x.Email == email);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateUser(string id, UserDto userDto)
    {
        var user = await this.GetUserById(id);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        await _userRepository.UpdateAsync(_mapper.Map<User>(userDto));
    }

    public async Task DeleteUser(string id)
    {
        var user = await _userRepository.GetAsync(x => x.Id.ToString() == id, false);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        await _userRepository.RemoveAsync(user);
    }
}