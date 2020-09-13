using System;
using Elk.Core;
using Elk.Http;
using GolPooch.SmsGateway;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GolPooch.Service.Implements
{
    public class AuthenticateService : IAuthenticateService
    {
        private ISmsGatway _smsGatway { get; set; }
        private AppUnitOfWork _appUow { get; set; }

        public AuthenticateService(AppUnitOfWork appUnitOfWork, ISmsGatway smsGatway)
        {
            _smsGatway = smsGatway;
            _appUow = appUnitOfWork;
        }

        private UserDeviceLog GetDeviceLog(long mobilenumber, HttpContext httpContext)
        {
            var requestDetails = ClientInfo.GetRequestDetails(httpContext);
            var ip = ClientInfo.GetIP(httpContext);
            var isMobile = requestDetails == null ? false : requestDetails.IsMobile;
            var os = $"{requestDetails?.OsName} {requestDetails?.OsVersion}";
            var device = $"{requestDetails?.Manufacture} {requestDetails?.Model}";
            var application = $"{requestDetails?.BrowserName} {requestDetails?.BrowserVersion}";

            return new UserDeviceLog
            {
                MobileNumber = mobilenumber,
                IsMobile = isMobile,
                IP = ip,
                Os = os.Length > 20 ? os.Substring(0, 20) : os,
                Device = device.Length > 50 ? device.Substring(0, 50) : device,
                Application = application.Length > 50 ? application.Substring(0, 50) : application
            };
        }

        public async Task<IResponse<int>> GetCodeAsync(long mobileNumber)
        {
            var response = new Response<int> { IsSuccessful = true, Message = ServiceMessage.Success };
            try
            {
                var randomPinCode = Randomizer.GetRandomInteger(4);
                var authenticate = new Authenticate
                {
                    IsUsed = false,
                    PinCode = 1234,
                    MobileNumber = mobileNumber,
                    ExpirationTime = DateTime.Now.AddMinutes(5)
                };
                await _appUow.AuthenticateRepo.AddAsync(authenticate);

                var saveResult = await _appUow.ElkSaveChangesAsync();
                if (saveResult.IsSuccessful)
                {
                    try
                    {
                        var sendResult = await _smsGatway.SendAsync(mobileNumber.ToMobileNumber().ToString(), ServiceMessage.VerificationCode_GetCode.Replace("{code}", randomPinCode.ToString()));
                    }
                    catch { }
                    response.Result = authenticate.AuthenticateId;
                    //if (sendResult.IsSuccessful && sendResult.Result) response.Result = authenticate.AuthenticateId;
                    //else
                    //{
                    //    response.IsSuccessful = false;
                    //    response.Message = sendResult.Message;
                    //}
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Message = saveResult.Message;
                }

                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<User>> VerifyCodeAsync(int transactionId, int pinCode, HttpContext httpContext)
        {
            var response = new Response<User>();
            try
            {
                var expiredTime = DateTime.Now;
                var existedPinCode = await _appUow.AuthenticateRepo.FirstOrDefaultAsync(new QueryFilter<Authenticate> { Conditions = x => x.AuthenticateId == transactionId, AsNoTracking = false });
                if (existedPinCode.IsNull()) return new Response<User> { Message = ServiceMessage.InvalidId };
                if (existedPinCode.IsUsed) return new Response<User> { Message = ServiceMessage.UsedPinCode };
                if (existedPinCode.ExpirationTime < expiredTime) return new Response<User> { Message = ServiceMessage.ExpiredPinCode };
                if (existedPinCode.PinCode != pinCode) return new Response<User> { Message = ServiceMessage.InvalidPinCode };

                var existedUser = await _appUow.UserRepo.FirstOrDefaultAsync(new QueryFilter<User> { Conditions = x => x.MobileNumber == existedPinCode.MobileNumber });
                if (existedUser.IsNull())
                {
                    var newUser = new User
                    {
                        MobileNumber = existedPinCode.MobileNumber
                    };
                    await _appUow.UserRepo.AddAsync(newUser);

                    response.Result = newUser;
                }
                else
                {
                    response.Result = existedUser;
                }

                existedPinCode.IsUsed = true;
                existedPinCode.UsedTime = DateTime.Now;
                _appUow.AuthenticateRepo.Update(existedPinCode);

                await _appUow.UserDeviceRepo.AddAsync(GetDeviceLog(existedPinCode.MobileNumber, httpContext));

                var saveResult = await _appUow.ElkSaveChangesAsync();
                response.IsSuccessful = saveResult.IsSuccessful;
                response.Message = saveResult.Message;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }
    }
}