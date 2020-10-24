using System;
using Elk.Core;
using Elk.Http;
using System.IO;
using System.Linq;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Enum;
using GolPooch.CrossCutting;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class UserService : IUserService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IConfiguration _configuration;

        public UserService(AppUnitOfWork appUnitOfWork, IConfiguration configuration)
        {
            _appUow = appUnitOfWork;
            _configuration = configuration;
        }


        private ActivityLog GetActivityLog(long mobilenumber, ActivityLogType type, HttpContext httpContext)
        {
            var requestDetails = ClientInfo.GetRequestDetails(httpContext);
            var ip = ClientInfo.GetIP(httpContext);
            var isMobile = requestDetails == null ? false : requestDetails.IsMobile;
            var os = $"{requestDetails?.OsName} {requestDetails?.OsVersion}";
            var device = $"{requestDetails?.Manufacture} {requestDetails?.Model}";
            var application = $"{requestDetails?.BrowserName} {requestDetails?.BrowserVersion}";

            return new ActivityLog
            {
                MobileNumber = mobilenumber,
                IsMobile = isMobile,
                Type = type,
                IP = ip,
                Os = os.Length > 20 ? os.Substring(0, 20) : os,
                Device = device.Length > 50 ? device.Substring(0, 50) : device,
                Application = application.Length > 50 ? application.Substring(0, 50) : application
            };
        }

        public async Task<IResponse<bool>> UpdateProfileAsync(User user, UserDto userDto)
        {
            var response = new Response<bool>();
            using (var trans = await _appUow.Database.BeginTransactionAsync())
            {
                try
                {
                    #region Update User Profile
                    userDto.Balance = user.Balance;
                    userDto.MobileNumber = user.MobileNumber;
                    var existedUser = await _appUow.UserRepo.FirstOrDefaultAsync(
                        new QueryFilter<User>
                        {
                            AsNoTracking = false,
                            Conditions = x => x.UserId == user.UserId
                        });
                    if (existedUser.IsNull()) return new Response<bool> { Message = ServiceMessage.InvalidParameter };

                    existedUser.UpdateWith(userDto);
                    _appUow.UserRepo.Update(existedUser);
                    #endregion

                    #region Check Profile Is Complete
                    var profilePurchase = await _appUow.PurchaseRepo.AnyAsync(
                        new QueryFilter<Purchase>
                        {
                            Conditions = x => x.ProductOffer.Product.Type == ProductType.Profile,
                            IncludeProperties = new List<Expression<Func<Purchase, object>>> {
                                x=> x.ProductOffer,
                                x=> x.ProductOffer.Product
                            }
                        });

                    if (profilePurchase)
                    {
                        await _appUow.SaveChangesAsync();
                        await trans.CommitAsync();
                        return new Response<bool>
                        {
                            Result = true,
                            IsSuccessful = true,
                            Message = ServiceMessage.UpdateProfile
                        };
                    }
                    #endregion

                    #region Purchase Complete Profile Product
                    var defaultPaymentGateway = await _appUow.PaymentGatewayRepo.FirstOrDefaultAsync(
                        new QueryFilter<PaymentGateway>
                        {
                            Conditions = x => x.IsActive && x.IsDefault
                        });

                    var now = DateTime.Now;
                    var productOffer = await _appUow.ProductOfferRepo.FirstOrDefaultAsync(
                        new QueryFilter<ProductOffer>
                        {
                            Conditions = x => x.IsActive && x.Product.Type == ProductType.Profile && !x.Product.IsShow && x.Product.ExpirationDate > now,
                            IncludeProperties = new List<Expression<Func<ProductOffer, object>>> { x => x.Product }
                        });

                    var transaction = new PaymentTransaction
                    {
                        UserId = existedUser.UserId,
                        IsSuccess = true,
                        TrackingId = "0",
                        Status = ServiceMessage.Success,
                        Type = TransactionType.Purchase,
                        Price = productOffer.Price,
                        ProductOfferId = productOffer.ProductOfferId,
                        PaymentGatewayId = defaultPaymentGateway.PaymentGatewayId,
                        Description = ServiceMessage.CompleteProfile
                    };
                    await _appUow.PaymentTransactionRepo.AddAsync(transaction);
                    var transactionResult = await _appUow.ElkSaveChangesAsync();
                    if (!transactionResult.IsSuccessful)
                    {
                        await trans.RollbackAsync();
                        return new Response<bool> { Message = ServiceMessage.Error };
                    }

                    var purchase = new Purchase
                    {
                        UserId = existedUser.UserId,
                        UsedChance = 0,
                        IsFinished = false,
                        IsReFoundable = false,
                        Chance = productOffer.Chance,
                        ProductOfferId = productOffer.ProductOfferId,
                        PaymentTransactionId = transaction.PaymentTransactionId,
                        ExpireDateMi = DateTime.Now.AddDays(productOffer.UnUseDay),
                        ExpireDateSh = PersianDateTime.Parse(DateTime.Now.AddDays(productOffer.UnUseDay)).ToString(PersianDateTimeFormat.Date),
                    };
                    await _appUow.PurchaseRepo.AddAsync(purchase);
                    #endregion

                    var saveResult = await _appUow.ElkSaveChangesAsync();
                    response.IsSuccessful = saveResult.IsSuccessful;
                    response.Result = saveResult.IsSuccessful;
                    if (saveResult.IsSuccessful)
                    {
                        response.Message = ServiceMessage.InsertProfile.Fill(productOffer.Chance.ToString());
                        await trans.CommitAsync();
                    }
                    else
                    {
                        response.Message = ServiceMessage.Error;
                        await trans.RollbackAsync();
                    }
                    return response;
                }
                catch (Exception e)
                {
                    FileLoger.Error(e);
                    await trans.RollbackAsync();
                    response.Message = ServiceMessage.Exception;
                    return response;
                }
            }
        }

        public async Task<IResponse<UserDto>> GetProfileAsync(int userId)
        {
            var response = new Response<UserDto>();
            try
            {
                var user = await _appUow.UserRepo.FirstOrDefaultAsync(
                    new QueryFilterWithSelector<User, UserDto>
                    {
                        Conditions = x => x.UserId == userId,
                        Selector = x => new UserDto
                        {
                            MobileNumber = x.MobileNumber,
                            Balance = x.Balance,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Gender = x.Gender,
                            Region = x.Region,
                            Email = x.Email,
                            BirthdateSh = x.BirthdateSh,
                            ProfileImgUrl = _configuration["CustomSettings:ApiAddress"] + x.ProfileImgUrl
                        }
                    });
                if (user.IsNull()) return new Response<UserDto> { Message = ServiceMessage.InvalidParameter };

                response.Result = user;
                response.IsSuccessful = true;
                response.Message = ServiceMessage.Success;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<string>> UploadAwatarAsync(int userId, string fileName, byte[] fileBytes)
        {
            var response = new Response<string>();
            try
            {
                #region Find User
                var user = await _appUow.UserRepo.FindAsync(userId);
                if (user.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidParameter };
                #endregion

                #region Save Profile Awatar
                var fileExtension = Path.GetExtension(fileName);
                var createPathResult = HttpFileTools.GetPath("Awatar" + fileExtension, root: "UsersFile", objectId: userId.ToString(), fileNamePrefix: "Profile");
                var saveFileResult = HttpFileTools.Save(fileBytes, createPathResult.FullPath);
                if (string.IsNullOrEmpty(saveFileResult)) return new Response<string>() { Message = ServiceMessage.FileNotSaved };
                #endregion

                #region Update User
                user.ProfileImgUrl = createPathResult.RelativePath;
                _appUow.UserRepo.UpdateUnAttached(user);
                #endregion

                var saveResult = await _appUow.ElkSaveChangesAsync();
                response.Message = saveResult.Message;
                response.IsSuccessful = saveResult.IsSuccessful;
                response.Result = saveResult.IsSuccessful ? _configuration["CustomSettings:ApiAddress"] + user.ProfileImgUrl : null;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<bool>> LogActivityAsync(long mobileNumber, HttpContext httpContext, ActivityLogType type = ActivityLogType.Login)
        {
            var response = new Response<bool>();
            try
            {
                await _appUow.ActivityLogRepo.AddAsync(GetActivityLog(mobileNumber, type, httpContext));
                var saveResult = await _appUow.ElkSaveChangesAsync();

                response.Result = saveResult.IsSuccessful;
                response.Message = ServiceMessage.Success;
                response.IsSuccessful = saveResult.IsSuccessful;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<PagingListDetails<ActivityLog>>> GetActivityAsync(long mobileNumber, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<ActivityLog>>();
            try
            {
                var activities = await _appUow.ActivityLogRepo.GetPagingAsync(
                    new PagingQueryFilter<ActivityLog>
                    {
                        Conditions = x => x.MobileNumber == mobileNumber,
                        OrderBy = x => x.OrderByDescending(x => x.ActivityLogId),
                        PagingParameter = pagingParameter
                    });

                response.Result = activities;
                response.IsSuccessful = true;
                response.Message = ServiceMessage.Success;
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