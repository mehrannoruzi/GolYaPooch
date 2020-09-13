using Elk.Core;
using Elk.Cache;
using GolPooch.SmsGateway;
using GolPooch.DataAccess.Ef;
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
            #endregion

            return services;
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddContext<AppDbContext>(_configuration.GetConnectionString("AppDbContext"));
            services.AddScoped<AppUnitOfWork>();

            services.AddScoped<ISmsGatway, SmsGatway>();

            #region Base
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserde, UserService>();
            #endregion

            #region Draw
            services.AddScoped<IChestService, ChestService>();

            #endregion

            #region Messaging
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<INotificationService, NotificationService>();

            #endregion

            #region Payment
            //services.AddScoped<IPaymentService, PaymentService>();

            #endregion

            #region Product
            services.AddScoped<IPurchaseService, PurchaseService>();

            #endregion

            return services;
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();

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