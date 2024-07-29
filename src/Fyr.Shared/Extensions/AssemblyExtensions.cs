using System.Reflection;
using Fyr.Shared.DependencyInjection;

namespace Fyr.Shared.Extensions;
/// <summary>
/// 程序集扩展
/// </summary>
public static class AssemblyExtensions
{
    /// <summary>
    /// 加载所有程序集
    /// </summary>
    public static void LoadAllLoadFrom()
    {
        // 获取当前应用程序目录
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // 获取目录中的所有 DLL 文件
        var dllFiles = Directory.GetFiles(appDirectory, "*.dll");

        foreach (var dllFile in dllFiles)
        {
            // 尝试加载每个 DLL 文件
            try
            {
                Assembly.LoadFrom(dllFile);
            }
            catch (Exception)
            {
            }
        }
    }
    /// <summary>
    /// 获取所有程序集
    /// </summary>
    /// <returns></returns>
    public static Assembly[] GetAssemblies()
    {
        // 加载所有程序集
        LoadAllLoadFrom();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        // 过滤掉系统程序集
        var allAssemblies = assemblies
            .Where(assembly => assembly.FullName != null && !assembly.FullName.StartsWith("System")
                               && !assembly.FullName.StartsWith("Microsoft")
                               && !assembly.FullName.StartsWith("netstandard")
                               && !assembly.FullName.StartsWith("mscorlib")
                               && !assembly.FullName.StartsWith("WindowsBase")
                               && !assembly.FullName.StartsWith("PresentationCore")
                               && !assembly.FullName.StartsWith("PresentationFramework")
                               && !assembly.FullName.StartsWith("WindowsFormsIntegration"));

        return allAssemblies.ToArray();
    }

    public static Dictionary<Type, List<Type>> GetAllInterface()
    {
        var assemblies = GetAssemblies();
        Dictionary<Type, List<Type>> dic = [];
        var transientServices = GetImplementationsOfInterface<ITransient>(assemblies);
        var scopedServices = GetImplementationsOfInterface<IScoped>(assemblies);
        var singletonServices = GetImplementationsOfInterface<ISingleton>(assemblies);
        dic.Add(typeof(ITransient), transientServices.ToList());
        dic.Add(typeof(IScoped), scopedServices.ToList());
        dic.Add(typeof(ISingleton), singletonServices.ToList());
        return dic;





    }

    public static IEnumerable<Type> GetImplementationsOfInterface<TInterface>(Assembly[] assemblies)
    {
        return assemblies.SelectMany(assembly => assembly.GetTypes())
                         .Where(type => type.GetInterfaces().Contains(typeof(TInterface)) && !type.IsInterface && !type.IsAbstract);
    }
}