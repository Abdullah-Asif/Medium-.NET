using AutoMapper;
using Medium.Application.DTOs;
using Medium.Application.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.Endpoints;

public static class BlogEndpoints
{
     public static void RegisterBlogEndpoints(this WebApplication app)
    {
        var blogGroup = app.MapGroup("/api/blogs");
        blogGroup.MapGet("/", GetAllBlogs);
        blogGroup.MapPost("/", CreateBlog).RequireAuthorization();
    }

    private static async Task<Ok<List<BlogDto>>> GetAllBlogs(IBlogService blogService, [FromQuery(Name = "page")]int pageNumber = 1)
    {
        var pageQueryRequest = new PaginationQueryRequest { PageNumber = pageNumber };
        var result = await blogService.GetAllBlogs(pageQueryRequest);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Created, BadRequest>> CreateBlog([FromBody]BlogDto blogDto, IBlogService blogService)
    { 
        await blogService.CreateBlog(blogDto); 
        return TypedResults.Created();
    }
}