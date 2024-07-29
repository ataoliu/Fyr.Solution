using Fyr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Fyr.Infrastructure.Data.Context;
public class ApplicationDbContext : DbContext
{




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=rm-uf65yx8y150h9w1u67o.sqlserver.rds.aliyuncs.com,3433;Fyr=lat;User Id=lat;Password=@lat200101;";

        optionsBuilder.UseSqlServer(connectionString);
        base.OnConfiguring(optionsBuilder);
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 添加配置
        modelBuilder.Entity<UserEntity>().ToTable("Users");
        // 配置其他实体
    }
}