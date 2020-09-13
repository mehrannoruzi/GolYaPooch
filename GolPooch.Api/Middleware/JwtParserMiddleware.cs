using System;
using Elk.Core;
using System.Text;
using System.Linq;
using Elk.AspNetCore;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GolPooch.Api
{
    public class JwtParserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;

        public JwtParserMiddleware(RequestDelegate next, IJwtService jwtService,
            IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtService = jwtService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            try
            {
                if (token != null)
                {
                    var userClaims = _jwtService.GetClaimsPrincipal(token, _jwtSettings);
                    if (userClaims != null)
                        context.Request.Headers.Add("UserId", userClaims.FindFirstValue("UserId"));
                }
                else
                {
                    if (context.Request.Headers["UserId"].FirstOrDefault() != null)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/Json";
                        var bytes = Encoding.ASCII.GetBytes(new Response<object> { IsSuccessful = false, Message = "UnAuthorized Access To Api !. Token Not Sent.", ResultCode = 401 }.SerializeToJson());
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }

                await _next(context);
            }
            catch (Exception e)
            {
                byte[] bytes;
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/Json";
                if (e.Message.Contains("Lifetime validation failed"))
                {
                    #region Expired Token
                    var validationTime = _jwtService.GetTokenExpireTime(token, _jwtSettings);
                    bytes = Encoding.ASCII.GetBytes(new Response<object>
                    {
                        ResultCode = 401,
                        IsSuccessful = false,
                        Message = $"UnAuthorized Access To Api ! Token Has Expired. Token Was Valid From: {validationTime.ValidFrom} Until To: {validationTime.ValidTo}",
                    }.SerializeToJson());
                    #endregion
                }
                else
                {
                    #region Another Exception
                    bytes = Encoding.ASCII.GetBytes(new Response<object>
                    {
                        ResultCode = 401,
                        IsSuccessful = false,
                        Message = $"UnAuthorized Access To Api ! Token Has Invalid. {e.Message}",
                    }.SerializeToJson());
                    #endregion
                }

                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}