using System.IO;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private IUserService _userService { get; set; }

        public UserController(IUserService userService)
            => _userService = userService;


        [HttpPost]
        public async Task<JsonResult> UpdateProfileAsync(User user, [FromBody] UserDto userDto)
            => Json(await _userService.UpdateProfileAsync(user.UserId, userDto));

        [HttpPost]
        public async Task<JsonResult> UploadAwatarAsync(User user, IFormFile Avatar)
        {
            if (Avatar.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    Avatar.CopyTo(ms);
                    return Json(await _userService.UploadAwatarAsync(user.UserId, Avatar.FileName, ms.ToArray()));
                }
            }
            else return Json(new { IsSuccessful = false, Message = "هیچ فایلی آپلود نشده است" });
        }

        [HttpPost]
        public async Task<ActionResult> LogActivityAsync(User user, [FromBody] ActivityLog log)
            => Ok(await _userService.LogActivityAsync(user.MobileNumber, HttpContext, log.Type));
    }
}