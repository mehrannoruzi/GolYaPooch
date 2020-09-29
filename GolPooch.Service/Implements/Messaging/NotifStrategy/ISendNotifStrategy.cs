using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Implements
{
    public interface ISendNotifStrategy
    {
        Task SendAsync(Notification notification, AppUnitOfWork _appUow);
    }
}