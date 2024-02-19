using Medium.Application.DTOs;
using Medium.Application.Utilities;
using Medium.Domain.Entities;

namespace Medium.Application.Interfaces;

public interface IBlogService
{
    Task<List<BlogDto>> GetAllBlogs(PaginationQueryRequest paginationQueryRequest);
    Task<BlogDto> GetBlogById(string id);
    Task CreateBlog(BlogDto blogDto);
    Task UpdateBlog(string id, BlogDto blogDto);
    Task DeleteBlog(string id);
    Task<UserBlogsDto> GetUsersBlog(string userId, PaginationQueryRequest paginationQueryRequest);
}