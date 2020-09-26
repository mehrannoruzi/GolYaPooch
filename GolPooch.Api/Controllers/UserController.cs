using GolPooch.Domain.Dto;
using GolPooch.Api.Models;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Http;

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

    }
}