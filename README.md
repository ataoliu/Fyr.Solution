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

## 其他
- **create.sh**: OSX create bash 脚本 
- **chmod +x create.sh** && **./create.sh**
- **run.sh**: OSX run bash 脚本 
- **chmod +x run.sh** && **./run.sh**
