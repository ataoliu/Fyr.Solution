using Microsoft.Extensions.DependencyInjection;
namespace Fyr.Attribute;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class ServiceInjectAttribute :System.Attribute
{
    public ServiceInjectAttribute()
    {
    }

    public ServiceInjectAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }

    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;
}