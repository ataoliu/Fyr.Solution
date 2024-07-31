namespace Fyr.Extensions;
public static class TypeExtensions
{
    // 扩展方法，用于检查类型是否实现了某个接口
    public static bool IsImplementsOf(this Type type, Type interfaceType)
    {
        // 检查传入的类型是否为接口类型
        if (!interfaceType.IsInterface)
        {
            throw new ArgumentException("The specified type is not an interface.", nameof(interfaceType));
        }

        // 检查类型是否实现了指定的接口
        return type.GetInterfaces().Contains(interfaceType);
    }
}