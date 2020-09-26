using Quartz;
using System;
using System.Threading.Tasks;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GolPooch.Service.Quartz
{
    [DisallowConcurrentExecution]
    public class SendPushJob : IJob
    {
        private IServiceProvider _serviceProvider { get; }

        public SendPushJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var NotificationService = scope.ServiceProvider.GetService<INotificationService>();
                await NotificationService.SendNotificationsAsync();
            }

            await Task.CompletedTask;
        }
    }
}