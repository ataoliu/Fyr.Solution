using Fyr.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fyr.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController(IOrderAppService orderAppService) : BaseController
    {
        private readonly IOrderAppService _orderAppService = orderAppService;

        [HttpGet]
        public string GetTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        [HttpGet]
        public string GetCurrent()
        {
            return _orderAppService.GetCurrent();
        }
    }

}