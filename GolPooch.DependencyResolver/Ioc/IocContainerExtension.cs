using Quartz;
using Elk.Core;
using Elk.Cache;
using Quartz.Spi;
using Quartz.Impl;
using GolPooch.Domain;
using GolPooch.DataAccess.Ef;
using GolPooch.Service.Quartz;
using GolPooch.Service.Interfaces;
using GolPooch.Service.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GolPooch.DependencyResolver.Ioc
{
    public static class IocContainerExtension
    {
        public static IServiceCollection AddTransient(this IServiceCollection services, IConfiguration _configuration)
        {
            #region Repos
            services.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>));


            services.AddTransient<IRoundWinnerRepo, RoundWinnerRepo>();
            #endregion

            return services;
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddContext<AppDbContext>(_configuration.GetConnectionString("AppDbContext"));
            services.AddScoped<AppUnitOfWork>();

            #region Base
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserde, UserService>();
            #endregion

            #region Draw
            services.AddScoped<IChestService, ChestService>();
            services.AddScoped<IRoundService, RoundService>();
            services.AddScoped<IRoundWinnerService, RoundWinnerService>();

            #endregion

            #region Messaging
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IPushService, PushService>();

            #endregion

            #region Payment
            services.AddScoped<IPaymentService, PaymentService>();

            #endregion

            #region Product
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();

            #endregion

            return services;
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();

            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<SendNotificationJob>();
            services.AddSingleton(new JobSchedule(
                                    jobType: typeof(SendNotificationJob),
                                    cronExpression: _configuration["CustomSettings:SendNotificationCron"]));


            return services;
        }

        public static IServiceCollection AddContext<TDbContext>(this IServiceCollection serviceCollection, string conectionString) where TDbContext : DbContext
        {
            serviceCollection.AddDbContext<TDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(conectionString,
                            sqlServerOption =>
                            {
                                sqlServerOption.MaxBatchSize(1000);
                                sqlServerOption.CommandTimeout(null);
                                sqlServerOption.UseRelationalNulls(false);
                            });
                //.AddInterceptors(new DbContextInterceptors());
            });

            return serviceCollection;
        }
    }
}