
using System;
using Microsoft.Extensions.DependencyInjection;
namespace Fyr.Shared;
public class ServiceInjectAttribute : Attribute
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