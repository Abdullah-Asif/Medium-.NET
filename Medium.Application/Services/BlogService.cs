using AutoMapper;
using Medium.Application.DTOs;
using Medium.Application.Exceptions;
using Medium.Application.Interfaces;
using Medium.Application.Utilities;
using Medium.Domain.Entities;

namespace Medium.Application.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    

    public BlogService(IBlogRepository blogRepository, IMapper mapper, IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _blogRepository = blogRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }
    public async Task<List<BlogDto>> GetAllBlogs(PaginationQueryRequest paginationQueryRequest)
    {
        var blogs =  await _blogRepository.GetAllAsync(paginationQueryRequest);
        return _mapper.Map<List<BlogDto>>(blogs);
    }

    public async Task<BlogDto> GetBlogById(string id)
    {
        var blog = await _blogRepository.GetAsync(x => x.Id.ToString() == id, false);
        if (blog == null)
        {
            throw new NotFoundException("Blog not found");
        }
        return _mapper.Map<BlogDto>(blog);
    }

    public async Task CreateBlog(BlogDto blogDto)
    {
        blogDto.UserId = new Guid(_currentUserService.UserId);
        var blogEntity = _mapper.Map<Blog>(blogDto);
        await _blogRepository.CreateAsync(blogEntity);
    }

    public async Task UpdateBlog(string id, BlogDto blogDto)
    {
        try
        {
            var blog = await _blogRepository.GetAsync(x => x.Id.ToString() == id, false);
            if (blog == null)
            {
                throw new NotFoundException("Blog not found");
            }
            await _blogRepository.UpdateAsync(_mapper.Map<Blog>(blogDto));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error when updating the blog", e.Message);
        }
    }

    public async Task DeleteBlog(string id)
    {
        try
        {
            var blog = await _blogRepository.GetAsync(x => x.Id.ToString() == id, false);
            if (blog == null)
            {
                throw new NotFoundException("Blog not found");
            }
            await _blogRepository.RemoveAsync(blog);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error when deleting the blog", e.Message);
        }
    }

    public async Task<UserBlogsDto> GetUsersBlog(string userId, PaginationQueryRequest paginationQueryRequest)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var blogs = await _blogRepository.GetAllFilteredAsync(x => x.UserId.ToString() == userId, paginationQueryRequest, false);
            var userBlogDto = new UserBlogsDto
            {
                User = _mapper.Map<UserDto>(user),
                Blogs = _mapper.Map<List<BlogDto>>(blogs)
            };
            
            return userBlogDto;
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to fetching users blogs", e.Message);
            throw;
        }
    }
}