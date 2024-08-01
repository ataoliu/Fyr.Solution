using System.Reflection;
using Fyr.Attribute;
using Fyr.DependencyInjection;
using Fyr.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Fyr.AspNetCore;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 加载程序集
    /// </summary>
    /// <param name="services"></param>
    public static void LoadAssemblies(this IServiceCollection services)
    {
        Extensions.AssemblyExtensions.LoadAssemblies();
    }

    /// <summary>
    /// 自动扫描需要注册的服务，被ServiceInject标记的class可自动注入
    /// </summary>
    /// <param name="services"></param>
    public static void AutoRegisterServices(this IServiceCollection services)
    {
        services.LoadAssemblies();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        services.RegisterServiceByAttribute(assemblies);
        services.RegisterBackgroundService(assemblies);
    }
    /// <summary>
    /// 自动扫描需要注册的服务，被ServiceInject标记的class可自动注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    public static void AutoRegisterServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.RegisterServiceByAttribute(assemblies);
        services.RegisterBackgroundService(assemblies);
    }
    /// <summary>
    /// 通过 ServiceAttribute 批量注册服务
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterServiceByAttribute(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var types = assemblies.SelectMany(t => t.GetExportedTypes()).Where(t => (t.GetCustomAttributes(typeof(ServiceInjectAttribute), false).Length > 0 || t.GetInterfaces().Intersect([typeof(IScoped), typeof(ITransient), typeof(ISingleton)]).Any()) && t.IsClass && !t.IsAbstract);
        foreach (var type in types)
        {
            var typeInterface = type.GetInterfaces().Except([typeof(IScoped), typeof(ITransient), typeof(ISingleton)]).OrderBy(t => t.Name.HammingDistance(type.Name)).FirstOrDefault();
            if (typeInterface == null)
            {
                //服务非继承自接口的直接注入
                switch (type.GetCustomAttribute<ServiceInjectAttribute>()?.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.TryAddSingleton(type);
                        break;
                    case ServiceLifetime.Scoped:
                        services.TryAddScoped(type);
                        break;
                    case ServiceLifetime.Transient:
                        services.TryAddTransient(type);
                        break;
                    default:
                        if (type.IsImplementsOf(typeof(IScoped)))
                            services.TryAddScoped(type);
                        else if (type.IsImplementsOf(typeof(ISingleton)))
                            services.TryAddSingleton(type);
                        else if (type.IsImplementsOf(typeof(ITransient)))
                            services.TryAddTransient(type);
                        break;
                }
            }
            else
            {
                //服务继承自接口的和接口一起注入
                switch (type.GetCustomAttribute<ServiceInjectAttribute>()?.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.TryAddSingleton(type);
                        services.TryAddSingleton(typeInterface, type);
                        break;

                    case ServiceLifetime.Scoped:
                        services.TryAddScoped(type);
                        services.TryAddScoped(typeInterface, type);
                        break;

                    case ServiceLifetime.Transient:
                        services.TryAddTransient(type);
                        services.TryAddTransient(typeInterface, type);
                        break;

                    default:
                        if (type.IsImplementsOf(typeof(IScoped)))
                        {
                            services.TryAddScoped(type);
                            services.TryAddSingleton(typeInterface, type);
                        }
                        else if (type.IsImplementsOf(typeof(ISingleton)))
                        {
                            services.TryAddSingleton(type);
                            services.TryAddSingleton(typeInterface, type);
                        }
                        else if (type.IsImplementsOf(typeof(ITransient)))
                        {
                            services.TryAddTransient(type);
                            services.TryAddTransient(typeInterface, type);
                        }
                        break;
                }
            }

        }
    }

    private static void RegisterBackgroundService(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var types = new List<Type>();

        foreach (var assembly in assemblies)
        {
            try
            {
                types.AddRange(assembly.GetTypes()
                    .Where(t => typeof(BackgroundService).IsAssignableFrom(t) && !t.IsAbstract));
            }
            catch (ReflectionTypeLoadException ex)
            {
                // 处理加载类型时的异常
                foreach (var loaderException in ex.LoaderExceptions)
                {
                    Console.WriteLine(loaderException);
                }

                // 添加成功加载的类型
                types.AddRange(ex.Types.Where(t => t != null && typeof(BackgroundService).IsAssignableFrom(t) && !t.IsAbstract));
            }
        }

        foreach (var type in types)
        {
            services.TryAddSingleton(typeof(IHostedService), type);
        }
    }
}