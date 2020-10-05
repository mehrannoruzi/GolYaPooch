using System;
using Elk.Core;
using System.Linq;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Enum;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class NotificationService : INotificationService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IConfiguration _configuration;
        private static object lockObject = new object();

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

        public async Task<IResponse<int>> ReadAsync(int userId, int notificationId)
        {
            var response = new Response<int>();
            try
            {
                var notification = await _appUow.NotificationRepo.FirstOrDefaultAsync(
                    new QueryFilter<Notification>
                    {
                        AsNoTracking = false,
                        Conditions = x => x.NotificationId == notificationId
                    });
                if (notification == null) return new Response<int> { Message = ServiceMessage.InvalidNotificationId };
                if (notification.UserId != userId) return new Response<int> { Message = ServiceMessage.InvalidParameter };

                notification.IsRead = true;
                _appUow.NotificationRepo.Update(notification);
                var saveResult = await _appUow.ElkSaveChangesAsync();
                if (!saveResult.IsSuccessful) return new Response<int> { Message = saveResult.Message };
                return await UnReadCount(userId);
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<int>> UnReadCount(int userId)
        {
            var response = new Response<int>();
            try
            {
                response.Result = await _appUow.NotificationRepo.CountAsync(
                    new QueryFilter<Notification>
                    {
                        Conditions = x => x.UserId == userId && x.IsActive && !x.IsRead
                    });

                response.IsSuccessful = true;
                response.Message = ServiceMessage.Success;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<PagingListDetails<NotificationDto>>> GetTopNotifications(int userId, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<NotificationDto>>();
            try
            {
                var notifications = await _appUow.NotificationRepo.GetPagingAsync(
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
                            Text = x.Text,
                            InsertDate = PersianDateTime.Parse(x.InsertDateMi).ToString(PersianDateTimeFormat.DateShortTime)
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

        public PagingListDetails<Notification> GetUnProccesedNotifications()
        {
            var notifs = new PagingListDetails<Notification>();
            try
            {
                lock (lockObject)
                {
                    notifs = _appUow.NotificationRepo.GetPagingAsync(
                    new PagingQueryFilter<Notification>
                    {
                        AsNoTracking = false,
                        OrderBy = x => x.OrderByDescending(x => x.Priority),
                        Conditions = x => x.IsActive && !x.IsSent,
                        IncludeProperties = new List<Expression<Func<Notification, object>>> { x => x.User },
                        PagingParameter = new PagingParameter { PageNumber = 1, PageSize = int.Parse(_configuration["CustomSettings:SendNotificationThreadCount"]) }
                    }).Result;

                    if (notifs.Items.Any())
                    {
                        foreach (var notif in notifs.Items)
                            notif.IsSent = true;

                        _appUow.NotificationRepo.UpdateRange(notifs.Items);
                        _appUow.SaveChanges();
                    }

                    return notifs;
                }
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return notifs;
            }
        }

        public async Task SendNotificationsAsync()
        {
            try
            {
                var notifs = GetUnProccesedNotifications();
                if (notifs.Items.Any())
                {
                    Parallel.ForEach(notifs.Items, async notif =>
                    {
                        await SendNotifFactory.GetStrategy(notif.Type).SendAsync(notif, _appUow);
                    });

                    //foreach (var notif in notifs.Items)
                    //    await SendNotifFactory.GetStrategy(notif.Type).SendAsync(notif, _appUow);
                }

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                await Task.FromException(e);
            }
        }
    }
}