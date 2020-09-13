using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class BannerController : Controller
    {
        private IBannerService _bannerService { get; set; }

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet]
        public JsonResult All()
            => Json(_bannerService.GetAllAvailable());
    }
}