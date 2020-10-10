using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GolPooch.Service.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Returns Available ProductOffer
        /// this method must be cached
        /// </summary>
        /// <returns>list of available ProductOffer for purchase</returns>
        Task<IResponse<List<ProductOffer>>> GetAllAvailableAsync();
    }
}