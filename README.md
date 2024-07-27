# 目录结构说明

## Application 层
- **Commands**: 包含应用程序命令。
- **Queries**: 包含应用程序查询。
- **DTOs**: 包含数据传输对象。
- **Services**: 包含应用程序服务。

## Domain 层
- **Entities**: 包含领域实体。
- **ValueObjects**: 包含值对象。
- **Aggregates**: 包含聚合根。
- **Repositories**: 包含领域存储库接口。
- **Services**: 包含领域服务接口。
- **Events**: 包含领域事件。
- **Factories**: 包含领域工厂。

## Infrastructure 层
- **Data**: 包含数据上下文和存储库实现。
- **Services**: 包含基础设施服务实现。
- **Configuration**: 包含依赖注入配置。

## Web 层
- **Controllers**: 包含 Web 控制器。
- **Models**: 包含 Web 模型。
- **Views**: 包含视图文件。

## WebAPI 层
- **Controllers**: 包含 WebAPI 控制器。

## Shared 层
- **Helpers**: 包含辅助方法。
- **Extensions**: 包含扩展方法。

## Test 层
- **测试项目**: 包含测试用例文件和测试框架。



# 项目结构

## 目录结构

本项目遵循分层架构，以下是各层级的目录结构说明：

### 架构图

```plaintext
+------------------+
|   Presentation   |
|  (UI / API)      |
+------------------+
         |
         v
+------------------+
|   Application    |
|   (Services)     |
+------------------+
         |
         v
+------------------+
|     Domain       |
|  (Entities,      |
|   Value Objects, |
|   Services,      |
|   Repositories)  |
+------------------+
         |
         v
+------------------+
|  Infrastructure  |
|   (Repositories, |
|    Services,     |
|    Databases)    |
+------------------+
```


## 其他
- **create.sh**: OSX create bash 脚本 
- **chmod +x create.sh** && **./create.sh**
- **run.sh**: OSX run bash 脚本 
- **chmod +x run.sh** && **./run.sh**



## 如何使用

1. **创建解决方案**: 运行 `run.sh` 脚本以创建解决方案和项目结构。
2. **构建和运行**: 使用 `dotnet build` 和 `dotnet run` 命令构建和运行项目。
3. **测试**: 运行测试项目以验证功能，使用 `dotnet test` 命令执行测试。

## 贡献

欢迎对项目进行贡献！请遵循以下步骤：
1. Fork 本项目。
2. 提交 Pull Request。
3. 描述你的更改并提供相关信息。

## 许可证

本项目遵循 [MIT 许可证](LICENSE)。