using System;
using Elk.Core;
using System.Linq;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using GolPooch.DataAccess.Ef;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class ZarinPalStrategy : IPaymentStrategy
    {
        public async Task<IResponse<string>> CreateAsync(AppUnitOfWork _appUow, PaymentTransaction model, IConfiguration configuration)
        {
            //var currentUser = _userBusiness.Find(model.UserId);
            //ServicePointManager.Expect100Continue = false;
            //string uniqueIdentifier = string.Empty;

            //var paymentRequest = _zarinPal.PaymentRequest(
            //    MerchantID: gateway.MerchantId,
            //    Amount: model.Price,
            //    Description: $"پرداخت سفارش {model.OrderId}",
            //    Email: currentUser.Email,
            //    Mobile: currentUser.MobileNumber.ToString(),
            //    CallbackURL: AppSettings.TransactionRedirectUrl_ZarinPal,
            //    Authority: out uniqueIdentifier);

            //if (paymentRequest == 100)
            //{
            //    var transaction = _transactionBusiness.Do(new Transaction
            //    {
            //        OrderId = model.OrderId,
            //        Price = model.Price,
            //        PaymentGatewayId = model.PaymentGatewayId,
            //        Authority = uniqueIdentifier,
            //        Status = "100",
            //        InsertDateMi = DateTime.Now,
            //        InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date)
            //    });

            //    if (transaction.IsSuccessful)
            //    {
            //        return new ActionResponse<string>
            //        {
            //            IsSuccessful = true,
            //            Result = $"https://www.zarinpal.com/pg/StartPay/{uniqueIdentifier}"
            //        };
            //    }
            //}
            //return new ActionResponse<string>
            //{
            //    IsSuccessful = false,
            //    Message = LocalMessage.Exception
            //};

            throw new NotImplementedException();
        }

        public async Task<IResponse<string>> VerifyAsync(AppUnitOfWork _appUow, PaymentTransaction model, IConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}