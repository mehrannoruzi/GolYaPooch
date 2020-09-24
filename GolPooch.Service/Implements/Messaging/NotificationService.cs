using System;
using Elk.Core;
using System.Linq;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Enum;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class NotificationService : INotificationService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IConfiguration _configuration;

        public NotificationService(AppUnitOfWork appUnitOfWork, IConfiguration configuration)
        {
            _appUow = appUnitOfWork;
            _configuration = configuration;
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

        public async Task<IResponse<int>> AddClickAsync(int userId, int notificationId)
        {
            var response = new Response<int>();
            try
            {
                var notifDelivery = new NotificationDelivery
                {
                    UserId = userId,
                    NotificationId = notificationId,
                    Type = NotificationDeliveryType.Click
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

        public IResponse<PagingListDetails<NotificationDto>> GetTopNotifications(int userId, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<NotificationDto>>();
            try
            {
                var notifications = _appUow.NotificationRepo.GetPaging(
                    new PagingQueryFilterWithSelector<Notification, NotificationDto>
                    {
                        Conditions = x => x.UserId == userId && x.IsActive,
                        PagingParameter = pagingParameter,
                        OrderBy = x => x.OrderByDescending(x => x.NotificationId),
                        Selector = x => new NotificationDto
                        {
                            #region Set Notification Property
                            NotificationId = x.NotificationId,
                            UserId = x.UserId,
                            Type = x.Type,
                            Action = x.Action,
                            IsRead = x.IsRead,
                            ActionText = x.ActionText,
                            Subject = x.Subject,
                            ImageUrl = x.ImageUrl,
                            IconUrl = x.IconUrl,
                            ActionUrl = x.ActionUrl,
                            Text = x.Text
                            #endregion
                        }
                    });

                foreach (var notif in notifications.Items)
                    notif.ImageUrl = notif.ImageUrl != null
                        ? _configuration["CustomSettings:CdnAddress"] + notif.ImageUrl
                        : null;

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