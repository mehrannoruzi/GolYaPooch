using Elk.Core;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using System.Collections.Generic;

namespace GolPooch.Service.Interfaces
{
    public interface IBannerService
    {
        /// <summary>
        /// Returns Available Banner (please check expiration time & isactive ) 
        /// this method must cached and returned from cache.
        /// each record must be included with page property (include Page) 
        /// </summary>
        /// <returns>list of banner</returns>
        Task<IResponse<List<Banner>>> GetAllAvailable();
    }
}