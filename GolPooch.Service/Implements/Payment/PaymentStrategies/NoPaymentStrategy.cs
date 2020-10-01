using Elk.Core;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;

namespace GolPooch.Service.Implements
{
    public class NoPaymentStrategy : IPaymentStrategy
    {
        public async Task<IResponse<string>> CreateAsync(AppUnitOfWork _appUow, PaymentGateway gateway, PaymentTransaction model)
        {
            return await Task.FromResult(new Response<string> { Message = ServiceMessage.InvalidPaymentGateway });
        }

        public async Task<IResponse<string>> VerifyAsync(AppUnitOfWork _appUow, PaymentGateway gateway, PaymentTransaction model, object responseGateway = null)
        {
            return await Task.FromResult(new Response<string> { Message = ServiceMessage.InvalidPaymentGateway });
        }
    }
}