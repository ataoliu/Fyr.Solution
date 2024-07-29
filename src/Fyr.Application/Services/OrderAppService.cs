using Fyr.Shared.DependencyInjection;

namespace Fyr.Application.Services;
public class OrderAppService : IOrderAppService //: IOrderAppService
{
    public string GetCurrent()
    {
        return $"先在的时间是:{DateTime.Now:yyyy年MM月dd HH:mm:ss}";
    }
}
public interface IOrderAppService:ISingleton
{
    public string GetCurrent();
}