using GolPooch.Domain.Dto;
using GolPooch.Api.Models;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

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
        public async Task<JsonResult> UploadAwatarAsync(User user, IFormFile avatar)
        {
            using (var ms = new MemoryStream())
            {
                avatar.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                // act on the Base64 data
                return Json(await _userService.UploadAwatarAsync(user.UserId, avatar.FileName, fileBytes));
            }
        }
    }
}