using System.Linq.Expressions;
using Fyr.Domain.Aggregates;
using Fyr.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Fyr.Infrastructure.Data.Repositories;
/// <summary>
/// 仓储
/// </summary>
/// <typeparam name="T">实体</typeparam>
public class Repository<T> : IRepository<T> where T : EntityAggregateRoot
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    /// <summary>
    /// 根据主键查找单个实体
    /// </summary>
    /// <param name="id">实体主键</param>
    /// <returns>实体</returns>
    public async Task<T?> FindAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }
    /// <summary>
    /// 不跟踪的实体
    /// </summary>
    /// <returns></returns>
    public IQueryable<T> AsNoTracking()
    {
        return _dbSet.AsNoTracking();
    }
    /// <summary>
    /// 根据条件查找单个实体
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>实体</returns>
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    /// <summary>
    /// 根据条件获取实体列表
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>实体列表</returns>
    public async Task<IEnumerable<T>?> GetListAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }

    /// <summary>
    /// 添加单个实体
    /// </summary>
    /// <param name="entity">实体</param>
    public async Task AddAsync(T entity)
    {
        entity.Create();
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 添加多个实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    public async Task AddAsync(IEnumerable<T> entities)
    {
        entities.ToList().ForEach(x => x.Create());
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 更新单个实体
    /// </summary>
    /// <param name="entity">实体</param>
    public async Task UpdateAsync(T entity)
    {
        entity.Update();
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 更新多个实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    public async Task UpdateAsync(IEnumerable<T> entities)
    {
        entities.ToList().ForEach(x => x.Update());
        _dbSet.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 根据主键删除单个实体
    /// </summary>
    /// <param name="id">实体主键</param>
    public async Task DeleteAsync(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            entity.Delete();
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 删除单个实体
    /// </summary>
    /// <param name="entity">实体</param>
    public async Task DeleteAsync(T entity)
    {
        entity.Delete();
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 删除多个实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    public async Task DeleteAsync(IEnumerable<T> entities)
    {
        entities.ToList().ForEach(x => x.Delete());
        _dbSet.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }
    /// <summary>
    /// 根据主键删除单个实体
    /// </summary>
    /// <param name="id">实体主键</param>
    public async Task RemoveAsync(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 删除单个实体
    /// </summary>
    /// <param name="entity">实体</param>
    public async Task RemoveAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 删除多个实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    public async Task RemoveAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}
