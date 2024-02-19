using System.Linq.Expressions;
using Medium.Application.Interfaces;
using Medium.Application.Utilities;
using Medium.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medium.Infrastructure.Repositories;

public class SqlRepository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    public SqlRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<List<T>> GetAllAsync(PaginationQueryRequest paginationQueryRequest, bool tracked = true)
    {
        IQueryable<T> query = _dbSet;
        if (!tracked)
        {
            query = query.AsNoTracking();
        }
        return await query
            .Skip((paginationQueryRequest.PageNumber - 1) * paginationQueryRequest.PageSize)
            .Take(paginationQueryRequest.PageSize).ToListAsync();
    }

    public async Task<List<T>> GetAllFilteredAsync(Expression<Func<T, bool>> filter, PaginationQueryRequest paginationQueryRequest, bool tracked = true)
    {
        IQueryable<T> query = _dbSet;
        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query
            .Skip((paginationQueryRequest.PageNumber - 1) * paginationQueryRequest.PageSize)
            .Take(paginationQueryRequest.PageSize).ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>>? filter, bool tracked = true)
    {
        IQueryable<T> query = _dbSet;
        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync();
    }
    
    public async Task<T> GetByIdAsync(string id)
    {
        var guidId = new Guid(id);
        return await _dbSet.FindAsync(guidId);
    }
 
    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveAsync();
    }

    public async Task UpdateAsync(T entity)
    {
         _dbSet.Update(entity);
         await SaveAsync();
    }
    public async Task RemoveAsync(T entity)
    {
        _dbSet.Remove(entity); 
        await SaveAsync();
    }

    public IQueryable<T> GetQueryableObject()
    {
        IQueryable<T> queryable = _dbSet;
        return queryable;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}