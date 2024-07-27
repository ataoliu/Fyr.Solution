clear 

webApiPID=""
rootPrefix="Fyr"
webApiName="$rootPrefix.WebAPI"

echo "webApiName:$webApiName"
# 确保 WebAPI 目录存在
if [ -d "src/$webApiName" ]; then
    echo "运行 WebAPI 项目..."
    cd src/$webApiName
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

