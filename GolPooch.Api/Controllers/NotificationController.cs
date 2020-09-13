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

        [HttpGet]
        public JsonResult Top(User user, [FromBody] PagingParameter pagingParameter)
            => Json(_notificationService.GetTopNotifications(user.UserId, pagingParameter));

        [HttpPost]
        public JsonResult Read(int notificationId)
            => Json(_notificationService.ReadAsync(notificationId));
    }
}