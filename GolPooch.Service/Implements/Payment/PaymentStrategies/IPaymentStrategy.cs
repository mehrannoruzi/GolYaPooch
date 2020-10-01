using Elk.Core;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Implements
{
    public interface IPaymentStrategy
    {
        Task<IResponse<string>> CreateAsync(AppUnitOfWork _appUow, PaymentGateway gateway, PaymentTransaction model);
        Task<IResponse<string>> VerifyAsync(AppUnitOfWork _appUow, PaymentGateway gateway, PaymentTransaction model, object responseGateway = null);
    }
}