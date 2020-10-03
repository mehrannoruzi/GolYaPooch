using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Implements
{
    public class NoActionStrategy : ISendNotifStrategy
    {
        public async Task SendAsync(Notification notification, AppUnitOfWork _appUow)
        {
            await Task.CompletedTask;
        }
    }
}