using System;
using Elk.Core;
using System.Linq;
using GolPooch.Domain.Enum;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class NotificationService : INotificationService
    {
        private AppUnitOfWork _appUow { get; set; }

        public NotificationService(AppUnitOfWork appUnitOfWork)
        {
            _appUow = appUnitOfWork;
        }

        public async Task<IResponse<int>> AddDeliveryAsync(int userId, int notificationId)
        {
            var response = new Response<int>();
            try
            {
                var notifDelivery = new NotificationDelivery
                {
                    UserId = userId,
                    NotificationId = notificationId,
                    Type = NotificationDeliveryType.Deliver
                };
                await _appUow.NotificationDeliveryRepo.AddAsync(notifDelivery);
                var saveResult = await _appUow.ElkSaveChangesAsync();
                response.Message = saveResult.Message;
                response.IsSuccessful = saveResult.IsSuccessful;
                response.Result = saveResult.IsSuccessful ? notifDelivery.NotificationDeliveryId : 0;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public IResponse<PagingListDetails<Notification>> GetTopNotifications(int userId, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<Notification>>();
            try
            {
                var notifications = _appUow.NotificationRepo.GetPaging(
                    new PagingQueryFilter<Notification>
                    {
                        Conditions = x => x.UserId == userId,
                        PagingParameter = pagingParameter,
                        OrderBy = x => x.OrderByDescending(x => x.NotificationId)
                    });

                response.Message = ServiceMessage.Success;
                response.Result = notifications;
                response.IsSuccessful = true;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<bool>> ReadAsync(int notificationId)
        {
            var response = new Response<bool>();
            try
            {
                var notification = await _appUow.NotificationRepo.FirstOrDefaultAsync(
                    new QueryFilter<Notification>
                    {
                        AsNoTracking = false,
                        Conditions = x => x.NotificationId == notificationId
                    });
                if (notification == null) return new Response<bool> { Message = ServiceMessage.InvalidNotificationId };

                notification.IsRead = true;
                _appUow.NotificationRepo.Update(notification);
                var saveResult = await _appUow.ElkSaveChangesAsync();

                response.IsSuccessful = saveResult.IsSuccessful;
                response.Result = saveResult.IsSuccessful;
                response.Message = saveResult.Message;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }
    }
}