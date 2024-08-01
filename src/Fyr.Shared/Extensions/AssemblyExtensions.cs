using System.Reflection;
using System.Security.Cryptography;
using Fyr.DependencyInjection;

namespace Fyr.Extensions;
/// <summary>
/// 程序集扩展
/// </summary>
public static class AssemblyExtensions
{

    public static void LoadAssemblies()
    {
        // 获取入口程序集
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
        {
            _ = GetAllDependencies(entryAssembly).OrderBy(s => s.FullName).ToList();

        }
    }

    /// <summary>
    /// 获取程序集的依赖 程序集
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static HashSet<Assembly> GetAllDependencies(Assembly assembly)
    {
        var dependencies = new HashSet<Assembly>();
        var assembliesToProcess = new Queue<AssemblyName>(assembly.GetReferencedAssemblies());

        while (assembliesToProcess.Count > 0)
        {
            var assemblyName = assembliesToProcess.Dequeue();

            // Check if the assembly is already loaded
            var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().FullName == assemblyName.FullName);

            if (loadedAssembly != null)
            {
                if (dependencies.Add(loadedAssembly))
                {
                    foreach (var referencedAssemblyName in loadedAssembly.GetReferencedAssemblies())
                    {
                        if (!dependencies.Any(a => a.GetName().FullName == referencedAssemblyName.FullName))
                        {
                            assembliesToProcess.Enqueue(referencedAssemblyName);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    // Load the assembly and add to dependencies
                    var assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{assemblyName.Name}.dll");
                    if (File.Exists(assemblyPath))
                    {
                        loadedAssembly = Assembly.LoadFrom(assemblyPath);
                        if (dependencies.Add(loadedAssembly))
                        {
                            foreach (var referencedAssemblyName in loadedAssembly.GetReferencedAssemblies())
                            {
                                if (!dependencies.Any(a => a.GetName().FullName == referencedAssemblyName.FullName))
                                {
                                    assembliesToProcess.Enqueue(referencedAssemblyName);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load assembly: {assemblyName.FullName}");
                    Console.WriteLine(ex);
                }
            }
        }

        return dependencies;
    }

}