using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Implements
{
    public interface ISendNotifStrategy
    {
        Task SendAsync(Notification notification);
    }
}