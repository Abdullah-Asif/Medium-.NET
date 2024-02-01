namespace Medium.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string Address { get; set; } = "";
    public List<Blog>? Blogs { get; set; }

}