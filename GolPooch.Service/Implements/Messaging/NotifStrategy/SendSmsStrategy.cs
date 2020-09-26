using System;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using GolPooch.DataAccess.Ef;

namespace GolPooch.Service.Implements
{
    public class SendSmsStrategy : ISendNotifStrategy
    {
        private AppUnitOfWork _appUow { get; set; }

        public SendSmsStrategy(AppUnitOfWork appUow)
        {
            _appUow = appUow;
        }

        public async Task SendAsync(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
