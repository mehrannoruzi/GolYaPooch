using Elk.Core;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class NoPaymentStrategy : IPaymentStrategy
    {
        public async Task<IResponse<string>> CreateAsync(AppUnitOfWork _appUow, PaymentTransaction model, IConfiguration configuration)
        {
            return await Task.FromResult(new Response<string> { Message = ServiceMessage.InvalidPaymentGateway });
        }

        public async Task<IResponse<string>> VerifyAsync(AppUnitOfWork _appUow, PaymentTransaction model, IConfiguration configuration)
        {
            return await Task.FromResult(new Response<string> { Message = ServiceMessage.InvalidPaymentGateway });
        }
    }
}