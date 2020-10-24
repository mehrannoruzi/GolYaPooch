using Elk.Core;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GolPooch.Service.Interfaces
{
    public interface IPaymentService
    {
        Task<IResponse<List<PaymentGatwayDto>>> GetAllGateway();
        Task<IResponse<string>> CreateAsync(PaymentTransaction paymentTransaction);
        Task<IResponse<string>> VerifyAsync(int paymentTransactionId, string Status, string Authority);
        Task<IResponse<object>> ReFoundAsync(int userId, int price, string shebaCode, string fullName);
        Task<IResponse<PagingListDetails<object>>> GetTransactionsAsync(int userId, PagingParameter pagingParameter);
    }
}