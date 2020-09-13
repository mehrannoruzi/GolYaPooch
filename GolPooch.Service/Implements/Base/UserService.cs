using System;
using Elk.Core;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Enum;
using GolPooch.CrossCutting;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class UserService : IUserService
    {
        private AppUnitOfWork _appUow { get; set; }

        public UserService(AppUnitOfWork appUnitOfWork)
        {
            _appUow = appUnitOfWork;
        }

        public async Task<IResponse<int>> UpdateProfileAsync(int userId, UserDto userDto)
        {
            var response = new Response<int>();
            try
            {
                #region Update User Profile
                var existedUser = await _appUow.UserRepo.FirstOrDefaultAsync(
                    new QueryFilter<User>
                    {
                        AsNoTracking = false,
                        Conditions = x => x.UserId == userId
                    });
                existedUser.UpdateWith(userDto);
                _appUow.UserRepo.Update(existedUser);
                #endregion

                #region Purchase Complete Profile Product
                var defaultPaymentGateway = await _appUow.PaymentGatewayRepo.FirstOrDefaultAsync(
                    new QueryFilter<PaymentGateway>
                    {
                        Conditions = x => x.IsActive
                    });

                var productOffer = await _appUow.ProductOfferRepo.FirstOrDefaultAsync(
                    new QueryFilter<ProductOffer>
                    {
                        Conditions = x => x.IsActive && x.Product.Type == ProductType.Profile && !x.Product.IsShow && x.Product.ExpirationDate > DateTime.Now,
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
                    PaymentGatewayId = defaultPaymentGateway.PaymentGatewayId
                };
                await _appUow.PaymentTransactionRepo.AddAsync(transaction);

                var purchase = new Purchase
                {
                    UserId = existedUser.UserId,
                    UsedChance = 0,
                    IsFinished = false,
                    IsReFoundable = false,
                    Chance = productOffer.Chance,
                    ProductOfferId = productOffer.ProductOfferId,
                    PaymentTransactionId = transaction.PaymentTransactionId
                };
                await _appUow.PurchaseRepo.AddAsync(purchase);
                #endregion

                var saveResult = await _appUow.ElkSaveChangesAsync();
                response.Message = saveResult.Message;
                response.IsSuccessful = saveResult.IsSuccessful;
                response.Result = saveResult.IsSuccessful ? existedUser.UserId : 0;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<string>> UploadAwatarAsync(int userId, string fileExtension, byte[] fileBytes)
        {
            var response = new Response<string>();
            try
            {
                #region Find User
                var user = await _appUow.UserRepo.FindAsync(userId);
                if (user.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidParameter };
                #endregion

                #region Save Profile Awatar
                var fullPath = HttpFileTools.GetPath("Awatar" + fileExtension, objectId: userId.ToString(), fileNamePrefix: "Profile");
                var saveFileResult = HttpFileTools.Save(fileBytes, fullPath);
                #endregion

                #region Update User
                user.ProfileImgUrl = saveFileResult;
                _appUow.UserRepo.UpdateUnAttached(user);
                #endregion

                var saveResult = await _appUow.ElkSaveChangesAsync();
                response.Message = saveResult.Message;
                response.IsSuccessful = saveResult.IsSuccessful;
                response.Result = saveResult.IsSuccessful ? user.ProfileImgUrl : null;
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