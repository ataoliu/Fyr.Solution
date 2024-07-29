#!/bin/bash

# 定义项目名称和目录
rootPrefix="Fyr"
solutionName="$rootPrefix.Solution"
applicationName="$rootPrefix.Application"
domainName="$rootPrefix.Domain"
infrastructureName="$rootPrefix.Infrastructure"
webName="$rootPrefix.Web"
sharedName="$rootPrefix.Shared"
webApiName="$rootPrefix.WebAPI"
testName="$rootPrefix.Tests"


# 确保在根目录创建解决方案文件夹和文件
rootDir=$(pwd)
echo "创建解决方案文件夹 $solutionName..."
mkdir -p $rootDir/$solutionName/src $rootDir/$solutionName/test
echo "rootDir:$rootDir"
# 进入解决方案目录
cd $rootDir/$solutionName

# 创建解决方案文件夹和文件
# echo "创建解决方案文件夹和文件..."


# mkdir -p $solutionName/src $solutionName/test

# 进入解决方案目录
cd $solutionName

# 创建解决方案文件

if [ ! -f "$solutionName.sln" ]; then
# 创建解决方案文件夹和文件
echo "创建解决方案文件夹和文件..."

    echo "创建解决方案文件..."
    dotnet new sln -n $solutionName
# else
#     echo "解决方案文件 $solutionName.sln 已存在"
fi

# 在 src 目录中创建项目结构
cd src

# 创建 Application 层
if [ ! -d $applicationName ]; then
    echo "创建 Application 层..."
    dotnet new classlib -n $applicationName
    mkdir -p $applicationName/Commands $applicationName/Queries $applicationName/DTOs $applicationName/Services
    [ ! -f $applicationName/Commands/OrderCommands.cs ] && touch $applicationName/Commands/OrderCommands.cs
    [ ! -f $applicationName/Queries/OrderQueries.cs ] && touch $applicationName/Queries/OrderQueries.cs
    [ ! -f $applicationName/DTOs/OrderDto.cs ] && touch $applicationName/DTOs/OrderDto.cs
    [ ! -f $applicationName/Services/OrderAppService.cs ] && touch $applicationName/Services/OrderAppService.cs
    [ -f $applicationName/Class1.cs ] && rm $applicationName/Class1.cs
fi


# 创建 Domain 层
if [ ! -d $domainName ]; then
    echo "创建 Domain 层..."
    dotnet new classlib -n $domainName
    mkdir -p $domainName/Entities $domainName/ValueObjects $domainName/Aggregates $domainName/Repositories $domainName/Services $domainName/Events $domainName/Factories
    [ ! -f $domainName/Entities/Order.cs ] && touch $domainName/Entities/Order.cs
    [ ! -f $domainName/ValueObjects/Address.cs ] && touch $domainName/ValueObjects/Address.cs
    [ ! -f $domainName/Aggregates/OrderAggregate.cs ] && touch $domainName/Aggregates/OrderAggregate.cs
    [ ! -f $domainName/Repositories/IOrderRepository.cs ] && touch $domainName/Repositories/IOrderRepository.cs
    [ ! -f $domainName/Services/IOrderDomainService.cs ] && touch $domainName/Services/IOrderDomainService.cs
    [ ! -f $domainName/Events/OrderCreatedEvent.cs ] && touch $domainName/Events/OrderCreatedEvent.cs
    [ ! -f $domainName/Factories/OrderFactory.cs ] && touch $domainName/Factories/OrderFactory.cs
    [ -f $domainName/Class1.cs ] && rm $domainName/Class1.cs
fi

# 创建 Infrastructure 层
if [ ! -d $infrastructureName ]; then
    echo "创建 Infrastructure 层..."
    dotnet new classlib -n $infrastructureName
    mkdir -p $infrastructureName/Data/Context $infrastructureName/Data/Repositories $infrastructureName/Services $infrastructureName/Configuration
    [ ! -f $infrastructureName/Data/Context/ApplicationDbContext.cs ] && touch $infrastructureName/Data/Context/ApplicationDbContext.cs
    [ ! -f $infrastructureName/Data/Repositories/OrderRepository.cs ] && touch $infrastructureName/Data/Repositories/OrderRepository.cs
    [ ! -f $infrastructureName/Services/ExternalOrderService.cs ] && touch $infrastructureName/Services/ExternalOrderService.cs
    [ ! -f $infrastructureName/Configuration/DependencyInjection.cs ] && touch $infrastructureName/Configuration/DependencyInjection.cs
    [ -f $infrastructureName/Class1.cs ] && rm $infrastructureName/Class1.cs
fi


# 创建 Web 层
if [ ! -d $webName ]; then
    echo "创建 Web 层..."
    dotnet new mvc -n $webName
    mkdir -p $webName/Controllers $webName/Models $webName/Views
    [ ! -f $webName/Controllers/OrderController.cs ] && touch $webName/Controllers/OrderController.cs
    [ ! -f $webName/Models/OrderViewModel.cs ] && touch $webName/Models/OrderViewModel.cs
    [ ! -f $webName/Views/OrderView.cshtml ] && touch $webName/Views/OrderView.cshtml
    [ -f $webName/Class1.cs ] && rm $webName/Class1.cs
fi

# 创建 WebAPI 层
if [ ! -d $webApiName ]; then
    echo "创建 WebAPI 层..."
    dotnet new webapi -n $webApiName -o $webApiName
    mkdir -p $webApiName/Controllers
    [ ! -f $webApiName/Controllers/OrderApiController.cs ] && touch $webApiName/Controllers/OrderApiController.cs
    [ -f $webApiName/Class1.cs ] && rm $webApiName/Class1.cs
fi

# 创建 Shared 层
if [ ! -d $sharedName ]; then
    echo "创建 Shared 层..."
    dotnet new classlib -n $sharedName
    mkdir -p $sharedName/Helpers $sharedName/Extensions
    [ ! -f $sharedName/Helpers/ValidationHelper.cs ] && touch $sharedName/Helpers/ValidationHelper.cs
    [ ! -f $sharedName/Extensions/ServiceCollectionExtensions.cs ] && touch $sharedName/Extensions/ServiceCollectionExtensions.cs
    [ -f $sharedName/Class1.cs ] && rm $sharedName/Class1.cs
fi

# 创建测试项目
cd ../test

if [ ! -d $testName ]; then
    echo "创建测试项目..."
    dotnet new xunit -n $testName
    # [ -f $testName/$testName.csproj ] && mv $testName/$testName.csproj $testName/
    [ -f $testName/Class1.cs ] && rm $testName/Class1.cs
fi


# 回到解决方案根目录
cd ../
# echo "将项目添加到解决方案中..."
dotnet sln $solutionName.sln add src/$applicationName/$applicationName.csproj
dotnet sln $solutionName.sln add src/$domainName/$domainName.csproj
dotnet sln $solutionName.sln add src/$infrastructureName/$infrastructureName.csproj
dotnet sln $solutionName.sln add src/$webName/$webName.csproj
dotnet sln $solutionName.sln add src/$webApiName/$webApiName.csproj
dotnet sln $solutionName.sln add src/$sharedName/$sharedName.csproj
dotnet sln $solutionName.sln add test/$testName/$testName.csproj

# 创建一个示例测试用例
if [ ! -f test/$testName/UnitTest1.cs ]; then
    echo "创建示例测试用例..."
    cat <<EOL > test/$testName/UnitTest1.cs
using Xunit;

namespace $testName
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var expected = 1;
            var actual = 1;

            // Act

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
EOL
# else
    # echo "示例测试用例文件 UnitTest1.cs 已存在"
fi

# echo "测试项目结构创建完成"

# 创建描述文件
if [ ! -f 目录结构说明.md ]; then
    echo "创建描述文件..."
    description="# 目录结构说明

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
" 
    echo "$description" > 目录结构说明.md
    echo "目录结构说明.md 文件已创建"
# else
    # echo "描述文件 目录结构说明.md 已存在"
fi

# echo "脚本执行完成"









# 还原所有项目的依赖项
echo "还原所有项目的依赖项..."
dotnet restore

# 构建所有项目
echo "构建所有项目..."
dotnet build

# # 运行 Web 项目
# echo "运行 Web 项目..."
# cd src/$webName
# dotnet run &

# 运行 WebAPI 项目
# 回到解决方案根目录
cd ../
webApiPID=""
# 确保 WebAPI 目录存在
if [ -d "$solutionName/src/$webApiName" ]; then
    echo "运行 WebAPI 项目..."
   echo $solutionName/src/$webApiName
    cd $solutionName/src/$webApiName
    dotnet run &
    webApiPID=$!  # 记录 WebAPI 的 PID
    disown        # 从当前 shell 中分离进程

fi

# 确保 WebAPI 目录存在
if [ -d "$src/$webApiName" ]; then
    echo "运行 WebAPI 项目..."
    cd $src/$webApiName
    dotnet run &
    webApiPID=$!  # 记录 WebAPI 的 PID
    disown        # 从当前 shell 中分离进程

fi

# 回到根目录
cd ../..



# 设置 trap 以捕获退出信号并终止 WebAPI 进程
trap "echo '停止 WebAPI 项目...'; kill $webApiPID 2>/dev/null; exit 0" SIGINT SIGTERM

# 等待用户输入以允许手动退出
echo "WebAPI 项目正在运行。按 Ctrl+C 以停止 WebAPI 项目并退出脚本... PID $webApiPID"
while true; do
    sleep 1
done

