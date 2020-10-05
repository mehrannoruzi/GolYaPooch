﻿using Elk.Core;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class NotificationController : Controller
    {
        private INotificationService _notificationService { get; set; }

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<JsonResult> AddDeliveryAsync(User user, int notificationId)
            => Json(await _notificationService.AddDeliveryAsync(user.UserId, notificationId));

        [HttpPost]
        public async Task<JsonResult> AddClickAsync(User user, int notificationId)
            => Json(await _notificationService.AddClickAsync(user.UserId, notificationId));

        [HttpPost]
        public async Task<JsonResult> Read(User user, int notificationId)
            => Json(await _notificationService.ReadAsync(user.UserId, notificationId));

        [HttpGet]
        public async Task<JsonResult> UnReadCount(User user)
            => Json(await _notificationService.UnReadCount(user.UserId));

        [HttpGet]
        public async Task<JsonResult> Top(User user, [FromQuery] PagingParameter pagingParameter)
            => Json(await _notificationService.GetTopNotifications(user.UserId, pagingParameter));
    }
}