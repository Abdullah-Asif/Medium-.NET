namespace Medium.Application.DTOs;

public class UserBlogsDto
{
    public UserDto User { get; set; }
    public List<BlogDto> Blogs { get; set; }
}