using Quartz;
using System;
using System.Threading.Tasks;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GolPooch.Service.Quartz
{
    [DisallowConcurrentExecution]
    public class PaybackPurchaseJob : IJob
    {
        private IServiceProvider _serviceProvider { get; }

        public PaybackPurchaseJob(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;


        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var purchaseService = scope.ServiceProvider.GetService<IPurchaseService>();
                await purchaseService.ProccessPaybackblePurchaseAsync();
            }

            await Task.CompletedTask;
        }
    }
}