using Microsoft.AspNetCore.Mvc;

namespace Fyr.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public  abstract class BaseController : ControllerBase
{

}