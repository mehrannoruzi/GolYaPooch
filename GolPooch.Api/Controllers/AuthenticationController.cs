using Elk.Core;
using Elk.AspNetCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Options;

namespace GolPooch.Api.Controllers
{
    [AuthFilter, Route("[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;
        private IAuthenticateService _authenticateService { get; }

        public AuthenticationController(IAuthenticateService authenticateService,
            IJwtService jwtService, IOptions<JwtSettings> jwtSettings)
        {
            _jwtService = jwtService;
            _jwtSettings = jwtSettings.Value;
            _authenticateService = authenticateService;
        }

        [HttpGet]
        public async Task<JsonResult> GetCodeAsync(long mobileNumber)
            => Json(await _authenticateService.GetCodeAsync(mobileNumber));

        [HttpPost]
        public async Task<JsonResult> VerifyCodeAsync(int transactionId, int pinCode)
        {
            var response = new Response<JwtToken>();
            var verifyResult = await _authenticateService.VerifyCodeAsync(transactionId, pinCode, HttpContext);
            if (verifyResult.IsSuccessful)
            {
                var userClaims = new List<Claim> {
                     new Claim("UserId", verifyResult.Result.UserId.ToString()),
                     new Claim("MobileNumber", verifyResult.Result.MobileNumber.ToString()),
                };

                response.IsSuccessful = true;
                response.Message = verifyResult.Message;
                response.Result = _jwtService.CreateToken(userClaims, _jwtSettings);
            }
            else
            {
                response.Message = verifyResult.Message;
            }

            return Json(response);
        }
    }
}