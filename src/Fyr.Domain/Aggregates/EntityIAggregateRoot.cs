using System.ComponentModel.DataAnnotations;
namespace Fyr.Domain.Aggregates;
/// <summary>
///   不允许没有主键
/// </summary>
/// <typeparam name="T">主键类型</typeparam>
public abstract class EntityIAggregateRoot<T> : EntityAggregateRoot
{
    /// <summary>
    /// 主键
    /// </summary>
    [Key]
    public T Id { get; protected set; } = default!;

    /// <summary>
    /// 删除标识
    /// </summary>
    public bool Deleted { get; set; } = false;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }


    /// <summary>
    /// 标记实体为删除
    /// </summary>
    public override void Delete()
    {
        Deleted = true;
        // Optionally, update UpdatedAt here
        UpdatedAt = DateTime.UtcNow;
        base.Delete();
    }
    /// <summary>
    /// 创建实体
    /// </summary>
    public override void Create()
    {
        CreatedAt = DateTime.UtcNow;
        base.Create();
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    public override void Update()
    {
        UpdatedAt = DateTime.UtcNow;
        base.Update();
    }
}
/// <summary>
/// 实体聚合根
/// </summary>
public abstract class EntityAggregateRoot
{
    /// <summary>
    /// 标记实体为删除
    /// </summary>
    public virtual void Delete()
    {
    }
    /// <summary>
    /// 创建实体
    /// </summary>
    public virtual void Create()
    {
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    public virtual void Update()
    {
    }
}
