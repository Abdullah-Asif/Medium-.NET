using System.Linq.Expressions;
using Medium.Application.Interfaces;
using Medium.Domain.Entities;

namespace Medium.Infrastructure.Repositories;

public class UserRepository: SqlRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}