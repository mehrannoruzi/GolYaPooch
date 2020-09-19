using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Interfaces
{
    public interface IPushService
    {
        Task<IResponse<bool>> Subscribe(PushEndpoint model);
        Task<IResponse<bool>> Unsubscribe(int userId);
        Task<IResponse<bool>> Unsubscribe(string P256DhSecretKey);
        Task<IResponse<bool>> Send(Notification notification);
    }
}