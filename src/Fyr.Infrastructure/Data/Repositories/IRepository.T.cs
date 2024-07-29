using System.Linq.Expressions;
using Fyr.Domain.Aggregates;

namespace Fyr.Infrastructure.Data.Repositories;
/// <summary>
/// 仓储
/// </summary>
/// <typeparam name="T">实体</typeparam>
public interface IRepository<T> where T : EntityAggregateRoot
{
    /// <summary>
    /// 根据主键查找单个实体
    /// </summary>
    /// <param name="id">实体主键</param>
    /// <returns>实体</returns>
    Task<T?> FindAsync(object id);

    /// <summary>
    /// 根据条件查找单个实体
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>实体</returns>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    /// <summary>
    /// 不跟踪的实体
    /// </summary>
    /// <returns></returns>
     IQueryable<T> AsNoTracking();

    /// <summary>
    /// 根据条件获取实体列表
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>实体列表</returns>
    Task<IEnumerable<T>?> GetListAsync(Expression<Func<T, bool>> expression);

    /// <summary>
    /// 添加单个实体
    /// </summary>
    /// <param name="entity">实体</param>
    Task AddAsync(T entity);

    /// <summary>
    /// 添加多个实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    Task AddAsync(IEnumerable<T> entities);

    /// <summary>
    /// 更新单个实体
    /// </summary>
    /// <param name="entity">实体</param>
    Task UpdateAsync(T entity);

    /// <summary>
    /// 更新多个实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    Task UpdateAsync(IEnumerable<T> entities);

    /// <summary>
    /// 根据主键删除单个实体
    /// </summary>
    /// <param name="id">实体主键</param>
    Task DeleteAsync(object id);

    /// <summary>
    /// 删除单个实体
    /// </summary>
    /// <param name="entity">实体</param>
    Task DeleteAsync(T entity);

    /// <summary>
    /// 删除多个实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    Task DeleteAsync(IEnumerable<T> entities);

    /// <summary>
    /// 根据主键删除单个实体
    /// </summary>
    /// <param name="id">实体主键</param>
    Task RemoveAsync(object id);

    /// <summary>
    /// 删除单个实体
    /// </summary>
    /// <param name="entity">实体</param>
    Task RemoveAsync(T entity);

    /// <summary>
    /// 删除多个实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    Task RemoveAsync(IEnumerable<T> entities);
}
