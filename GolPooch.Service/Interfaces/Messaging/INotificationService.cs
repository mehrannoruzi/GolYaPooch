using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Interfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Add delivery of notification in notificationDelivery Table
        /// </summary>
        /// <param name="userId">read from jwt</param>
        /// <param name="notificationId">get this id from push message</param>
        /// <returns>return notification id</returns>
        Task<IResponse<int>> AddDeliveryAsync(int userId, int notificationId);

        /// <summary>
        /// Get top 10 notifications with userid and all notification that sets userid with null value.
        /// </summary>
        /// <param name="userId">userid in jwt</param>
        /// <returns>Top 10 records of Notification</returns>
        IResponse<PagingListDetails<Notification>> GetTopNotifications(int userId, PagingParameter pagingParameter);

        /// <summary>
        /// When user tapped in a notification, this method called to set isread field with true status.
        /// </summary>
        /// <param name="userId">read from jwt</param>
        /// <param name="notification">notification primary key</param>
        /// <returns>return notification id</returns>
        Task<IResponse<bool>> ReadAsync(int notificationId);
    }
}