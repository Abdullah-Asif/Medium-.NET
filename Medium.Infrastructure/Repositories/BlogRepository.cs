using Medium.Application.Interfaces;
using Medium.Domain.Entities;

namespace Medium.Infrastructure.Repositories;

public class BlogRepository: SqlRepository<Blog>, IBlogRepository
{
    public BlogRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}