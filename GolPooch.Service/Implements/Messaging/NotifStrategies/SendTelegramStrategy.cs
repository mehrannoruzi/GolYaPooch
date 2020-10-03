using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using GolPooch.DataAccess.Ef;

namespace GolPooch.Service.Implements
{
    public class SendTelegramStrategy : ISendNotifStrategy
    {
        public async Task SendAsync(Notification notification, AppUnitOfWork _appUow)
        {
            await Task.CompletedTask;
        }
    }
}