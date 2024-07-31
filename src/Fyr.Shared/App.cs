using System.Reflection;
namespace Fyr.Shared
{
    public static class App
    {
        public static List<Assembly> Assemblies
        {
            get
            {
                return [.. AppDomain.CurrentDomain.GetAssemblies()];
            }
        }
        
    }
}