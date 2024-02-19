namespace Medium.Application.Interfaces;

public interface ICurrentUserService
{
    public string UserId { get; }
    public string Email { get; }
}