﻿using System;
using Elk.Core;
using System.IO;
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

        public async Task<IResponse<int>> UpdateProfileAsync(int userId, UserDto userDto)
        {
            var response = new Response<int>();
            using (var trans = await _appUow.Database.BeginTransactionAsync())
            {
                try
                {
                    #region Update User Profile
                    var existedUser = await _appUow.UserRepo.FirstOrDefaultAsync(
                        new QueryFilter<User>
                        {
                            AsNoTracking = false,
                            Conditions = x => x.UserId == userId
                        });
                    if (existedUser.IsNull()) return new Response<int> { Message = ServiceMessage.InvalidParameter };

                    existedUser.UpdateWith(userDto);
                    _appUow.UserRepo.Update(existedUser);
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
                    await _appUow.ElkSaveChangesAsync();

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
                    await trans.RollbackAsync();
                    response.Message = ServiceMessage.Exception;
                    return response;
                }
                finally
                {
                    await trans.CommitAsync();
                }
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
    }
}