using Elk.Core;
using GolPooch.Domain.Entity;
using Elk.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GolPooch.DataAccess.Ef
{
    public partial class AppUnitOfWork : IElkUnitOfWork
    {
        #region Base
        public IGenericRepo<Authenticate> AuthenticateRepo => _serviceProvider.GetService<IGenericRepo<Authenticate>>();
        public IGenericRepo<ChangeLog> ChangeLogRepo => _serviceProvider.GetService<IGenericRepo<ChangeLog>>();
        public IGenericRepo<Page> PageRepo => _serviceProvider.GetService<IGenericRepo<Page>>();
        public IGenericRepo<Ticket> TicketRepo => _serviceProvider.GetService<IGenericRepo<Ticket>>();
        public IGenericRepo<User> UserRepo => _serviceProvider.GetService<IGenericRepo<User>>();
        public IGenericRepo<UserDeviceLog> UserDeviceRepo => _serviceProvider.GetService<IGenericRepo<UserDeviceLog>>();
        #endregion

        #region Draw
        public IGenericRepo<Chest> ChestRepo => _serviceProvider.GetService<IGenericRepo<Chest>>();
        public IGenericRepo<DrawChance> DrawChanceRepo => _serviceProvider.GetService<IGenericRepo<DrawChance>>();
        public IGenericRepo<Round> RoundRepo => _serviceProvider.GetService<IGenericRepo<Round>>();
        #endregion

        #region Messaging
        public IGenericRepo<Banner> BannerRepo => _serviceProvider.GetService<IGenericRepo<Banner>>();
        public IGenericRepo<Notification> NotificationRepo => _serviceProvider.GetService<IGenericRepo<Notification>>();
        public IGenericRepo<NotificationDelivery> NotificationDeliveryRepo => _serviceProvider.GetService<IGenericRepo<NotificationDelivery>>();
        #endregion

        #region Payment
        public IGenericRepo<PaymentGateway> PaymentGatewayRepo => _serviceProvider.GetService<IGenericRepo<PaymentGateway>>();
        public IGenericRepo<PaymentTransaction> PaymentTransactionRepo => _serviceProvider.GetService<IGenericRepo<PaymentTransaction>>();
        #endregion

        #region Product
        public IGenericRepo<Product> ProductRepo => _serviceProvider.GetService<IGenericRepo<Product>>();
        public IGenericRepo<ProductOffer> ProductOfferRepo => _serviceProvider.GetService<IGenericRepo<ProductOffer>>();
        public IGenericRepo<Purchase> PurchaseRepo => _serviceProvider.GetService<IGenericRepo<Purchase>>();
        #endregion
    }
}