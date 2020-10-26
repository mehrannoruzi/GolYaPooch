﻿using Quartz;
using System;
using System.Threading.Tasks;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GolPooch.Service.Quartz
{
    [DisallowConcurrentExecution]
    public class SendNotificationJob : IJob
    {
        private IServiceProvider _serviceProvider { get; }

        public SendNotificationJob(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;


        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var notificationService = scope.ServiceProvider.GetService<INotificationService>();
                await notificationService.SendNotificationsAsync();
            }

            await Task.CompletedTask;
        }
    }
}