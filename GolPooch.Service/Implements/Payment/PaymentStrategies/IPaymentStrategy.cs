using Elk.Core;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public interface IPaymentStrategy
    {
        Task<IResponse<string>> CreateAsync(AppUnitOfWork _appUow, PaymentTransaction model, IConfiguration configuration);
        Task<IResponse<string>> VerifyAsync(AppUnitOfWork _appUow, PaymentTransaction model, IConfiguration configuration);
    }
}