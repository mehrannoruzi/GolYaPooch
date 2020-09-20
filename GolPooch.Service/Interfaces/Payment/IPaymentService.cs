using Elk.Core;
using GolPooch.Domain.Dto;
using System.Collections.Generic;

namespace GolPooch.Service.Interfaces
{
    public interface IPaymentService
    {
        IResponse<List<PaymentGatwayDto>> GetAllGateway();
    }
}