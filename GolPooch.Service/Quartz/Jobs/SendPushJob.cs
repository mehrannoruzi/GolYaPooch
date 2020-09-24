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
                var pushService = scope.ServiceProvider.GetService<IPushService>();
                await pushService.SendPush();
            }

            await Task.CompletedTask;
        }
    }
}