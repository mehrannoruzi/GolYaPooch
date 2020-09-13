using System;
using Elk.Core;
using Elk.Http;
using System.Linq;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace GolPooch.Api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeFilter : ActionFilterAttribute, IAsyncActionFilter
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            try
            {
                var ActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
                bool skipAuthorize = ActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
                if (!skipAuthorize)
                {
                    var ip = ClientInfo.GetIP(filterContext.HttpContext);
                    if (filterContext.HttpContext.Request.Headers["UserId"].Count > 0)
                    {
                        #region Existing Token
                        var userId = int.Parse(filterContext.HttpContext.Request.Headers["UserId"][0]);
                        if (userId > 0)
                        {
                            var userRepo = (IGenericRepo<User>)filterContext.HttpContext.RequestServices.GetService(typeof(IGenericRepo<User>));
                            var user = await userRepo.FindAsync(userId);
                            if (user != null)
                            {
                                if (filterContext.ActionArguments.ContainsKey("User"))
                                    filterContext.ActionArguments["User"] = user;
                            }
                            else
                            {
                                FileLoger.Info($"Invalid Token Claims Data To Access Api !" + Environment.NewLine +
                                    $"IP: {ip}" + Environment.NewLine +
                                    $"UserId: {userId}" + Environment.NewLine +
                                    $"URL:{filterContext.HttpContext.Request.Path.Value}" + Environment.NewLine +
                                    $"UrlReferer:{filterContext.HttpContext.Request.GetTypedHeaders().Referer}");

                                filterContext.HttpContext.Response.StatusCode = 401;
                                filterContext.Result = new JsonResult(new Response<object>
                                {
                                    ResultCode = 401,
                                    IsSuccessful = false,
                                    Message = "UnAuthorized Access. Invalid Token Claims Data To Access Api !"
                                });
                            }
                        }
                        else
                        {
                            FileLoger.Info($"Invalid Token Data To Access Api !" + Environment.NewLine +
                                    $"IP: {ip}" + Environment.NewLine +
                                    $"UserId: {userId}" + Environment.NewLine +
                                    $"URL:{filterContext.HttpContext.Request.Path.Value}" + Environment.NewLine +
                                    $"UrlReferer:{filterContext.HttpContext.Request.GetTypedHeaders().Referer}");

                            filterContext.HttpContext.Response.StatusCode = 401;
                            filterContext.Result = new JsonResult(new Response<object>
                            {
                                ResultCode = 401,
                                IsSuccessful = false,
                                Message = "UnAuthorized Access. Invalid Token Data To Access Api !"
                            });
                        }
                        #endregion
                    }
                    else
                    {
                        #region Not Existing Token
                        FileLoger.Info($"UnAuthorized Access To Api. Token Not Sent." + Environment.NewLine +
                            $"IP: {ip}" + Environment.NewLine +
                            $"URL:{filterContext.HttpContext.Request.Path.Value}" + Environment.NewLine +
                            $"UrlReferer:{filterContext.HttpContext.Request.GetTypedHeaders().Referer}");

                        filterContext.HttpContext.Response.StatusCode = 403;
                        filterContext.Result = new JsonResult(new Response<object>
                        {
                            ResultCode = 403,
                            IsSuccessful = false,
                            Message = "UnAuthorized Access To Api. Token Not Sent.",
                        });
                        #endregion
                    }
                }

                await base.OnActionExecutionAsync(filterContext, next);
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new JsonResult(new Response<object>
                {
                    ResultCode = 500,
                    IsSuccessful = false,
                    Message = "Internall Error."
                });

                await base.OnActionExecutionAsync(filterContext, next);
            }
        }
    }
}
