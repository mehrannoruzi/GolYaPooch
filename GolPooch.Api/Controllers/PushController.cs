using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class PushController : Controller
    {
        private IPushService _pushService { get; set; }

        public PushController(IPushService pushService)
            => _pushService = pushService;


        [HttpPost]
        public async Task<IActionResult> SubscribeAsync(User user, [FromBody] PushEndpoint model)
        {
            model.UserId = user.UserId;
            return Json(await _pushService.SubscribeAsync(model));
        }
    }
}