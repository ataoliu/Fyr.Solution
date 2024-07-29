using Fyr.Domain.Aggregates;

namespace Fyr.Domain.Entities;
/// <summary>
/// 用户实体
/// </summary>
public class UserEntity : EntityIAggregateRoot<Guid>
{

    public string? Account { get; set; }
    public string? UserName { get; set; }





    /// <summary>
    /// 创建实体
    /// </summary>
    public override void Create()
    {
        Id = Guid.NewGuid();
        base.Create();
    }
    /// <summary>
    /// 更新实体
    /// </summary>
    public override void Update()
    {
        base.Update();
    }
    /// <summary>
    /// 删除实体
    /// </summary>
    public override void Delete()
    {
        base.Delete();
    }

}