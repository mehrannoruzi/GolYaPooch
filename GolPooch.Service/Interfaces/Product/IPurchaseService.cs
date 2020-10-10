using Elk.Core;
using GolPooch.Domain.Dto;
using System.Threading.Tasks;

namespace GolPooch.Service.Interfaces
{
    public interface IPurchaseService
    {
        Task<IResponse<PagingListDetails<PurchaseDto>>> GetActivePurchasesAsync(int userId, PagingParameter pagingParameter);
        Task<IResponse<PagingListDetails<PurchaseDto>>> GetAllPurchasesAsync(int userId, PagingParameter pagingParameter);
    }
}