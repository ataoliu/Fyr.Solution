using Fyr.Shared.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
namespace Fyr.Shared.Extensions;
/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 自动注入  Dependency Injection
    /// </summary>
    /// <param name="services"></param>
    public static void AddAutoDI(this IServiceCollection services)
    {
        var dic = AssemblyExtensions.GetAllInterface();
        foreach (var iface in dic)
        {
            //瞬态
            if (iface.Key == typeof(ITransient))
            {
                iface.Value.ForEach(type =>
                {
                    var interfaces = type.GetInterfaces().Where(s => s != iface.Key).ToArray();
                    foreach (var iface in interfaces)
                    {
                        services.AddTransient(iface, type);
                    }
                });
            }
            //作用域
            if (iface.Key == typeof(IScoped))
            {
                iface.Value.ForEach(type =>
                {
                    var interfaces = type.GetInterfaces().Where(s => s != iface.Key).ToArray();
                    foreach (var iface in interfaces)
                    {
                        services.AddScoped(iface, type);
                    }
                });
            }
            //单例
            if (iface.Key == typeof(ISingleton))
            {
                iface.Value.ForEach(type =>
                 {
                     var interfaces = type.GetInterfaces().Where(s => s != iface.Key).ToArray();
                     foreach (var iface in interfaces)
                     {
                         services.AddSingleton(iface, type);
                     }
                 });
            }
        }
        // foreach (var item in dic)
        // {
        //     services.AddScoped(item.Key, item.Value);
        // }


    }

}