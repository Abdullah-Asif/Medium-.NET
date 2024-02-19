using Medium.Application.DTOs;
using Medium.Application.Utilities;
using Medium.Domain.Entities;

namespace Medium.Application.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsers(PaginationQueryRequest paginationQueryRequest);
    Task CreateUser(SignUpDto signUpDto);
    Task<UserDto> GetUserById(string id);
    Task<UserDto> GetUserByName(string name);
    Task<UserDto> GetUserByEmail(string email);
    Task UpdateUser(string id, UserDto user);
    Task DeleteUser(string id);
}