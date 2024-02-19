using AutoMapper;
using Medium.Application.DTOs;
using Medium.Application.Interfaces;
using Medium.Application.Utilities;
using Medium.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

public static class UserEndpoints
{
    public static void RegisterUserEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/users").RequireAuthorization();
        userGroup.MapGet("/", GetAllUsers);
        userGroup.MapPost("/", CreateUser);
        userGroup.MapGet("{id}", GetById);
        userGroup.MapGet("email/{email}", GetByEmail);
        userGroup.MapGet("username/{username}", GetByUserName);
        userGroup.MapPut("{id}", UpdateUser);
        userGroup.MapDelete("{id}", DeleteUser);
        userGroup.MapGet("{userId}/blogs", GetUserBlog).AllowAnonymous();
        userGroup.MapGet("{userId}/blogs/{blogId}",GetBlogById);
        userGroup.MapPut("{userId}/blogs/{blogId}",UpdateBlog);
        userGroup.MapDelete("{userId}/blogs/{blogId}",DeleteBlog);
    }

    private static async Task<Ok<List<UserDto>>> GetAllUsers( IUserService userService,  [FromQuery(Name = "page")] int pageNumber = 1)
    {
        var paginationQuery = new PaginationQueryRequest { PageNumber = pageNumber };
        var result = await userService.GetAllUsers(paginationQuery);
        return TypedResults.Ok(result);
    }
    private static async Task<Ok<UserDto>> GetById(string id, IUserService userService, IMapper mapper)
    {
        var user = await userService.GetUserById(id);
        return TypedResults.Ok<UserDto>(mapper.Map<UserDto>(user));
    }
    private static async Task<Results<Created, BadRequest>> CreateUser([FromBody]SignUpDto signUpDto, IUserService userService)
    { 
        await userService.CreateUser(signUpDto); 
        return TypedResults.Created();
    }
    private static async Task<Ok<UserDto>> GetByUserName(string username, IUserService userService, IMapper mapper)
    {
        var user = await userService.GetUserByName(username);
        return TypedResults.Ok<UserDto>(user);
    }
    private static async Task<Ok<UserDto>> GetByEmail(string email, IUserService userService, IMapper mapper)
    {
        var user = await userService.GetUserByEmail(email);
        return TypedResults.Ok<UserDto>(user);
    }

    private static async Task<Ok<string>> UpdateUser(string id, [FromBody]UserDto userDto, IUserService userService)
    {
        await userService.UpdateUser(id, userDto); 
        return TypedResults.Ok("User updated successfully");
    }
    private static async Task<Ok<string>> DeleteUser(string id, IUserService userService)
    {
        await userService.DeleteUser(id);
        return TypedResults.Ok("User deleted successfully");
    }
    private static async Task<Ok<UserBlogsDto>> GetUserBlog(string userId,  IBlogService blogService, IMapper mapper, [FromQuery(Name = "page")] int pageNumber = 1)
    {
        var paginationQuery = new PaginationQueryRequest { PageNumber = pageNumber };
        var userBlogsDto = await blogService.GetUsersBlog(userId, paginationQuery);
        return TypedResults.Ok<UserBlogsDto>(userBlogsDto);
    }
    private static async Task<Ok<BlogDto>> GetBlogById(string userId, string blogId, IBlogService blogService, IMapper mapper)
    {
        var blog = await blogService.GetBlogById(blogId);
        return TypedResults.Ok<BlogDto>(blog);
    }
    private static async Task<Ok<string>> UpdateBlog(string blogId, [FromBody]BlogDto blogDto, IBlogService blogService)
    {
        await blogService.UpdateBlog(blogId, blogDto);
        return TypedResults.Ok("Blog updated successfully");
    }
    private static async Task<Ok<string>> DeleteBlog(string blogId, IBlogService blogService)
    {
        await blogService.DeleteBlog(blogId);
        return TypedResults.Ok("Blog deleted successfully");
    }
}
