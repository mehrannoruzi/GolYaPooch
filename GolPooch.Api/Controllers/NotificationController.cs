using Elk.Core;
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

        [HttpGet]
        public JsonResult Top(User user, [FromBody] PagingParameter pagingParameter)
            => Json(_notificationService.GetTopNotifications(user.UserId, pagingParameter));

        [HttpPost]
        public async Task<JsonResult> Read(int notificationId)
            => Json(await _notificationService.ReadAsync(notificationId));
    }
}