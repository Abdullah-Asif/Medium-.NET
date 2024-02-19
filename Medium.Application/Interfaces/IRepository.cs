using System.Linq.Expressions;
using Medium.Application.Utilities;
using Medium.Domain.Entities;

namespace Medium.Application.Interfaces;

public interface IRepository<TEntity>
{
    Task<List<TEntity>> GetAllAsync(PaginationQueryRequest paginationQueryRequest,bool tracked = true);
    Task<List<TEntity>> GetAllFilteredAsync(Expression<Func<TEntity, bool>> filter, PaginationQueryRequest paginationQueryRequest, bool tracked = true);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? filter, bool tracked = true);
    Task<TEntity> GetByIdAsync(string id);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    IQueryable<TEntity> GetQueryableObject();
    Task SaveAsync();
}