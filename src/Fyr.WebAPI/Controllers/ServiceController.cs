using Fyr.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fyr.WebAPI.Controllers;
public class ServiceController : BaseController
{
    private readonly IServiceProvider _serviceProvider;

    public ServiceController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }

}