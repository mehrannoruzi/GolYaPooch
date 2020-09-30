using Elk.Core;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GolPooch.SmsGateway
{
    public interface ISmsGatway : IScopedInjection
    {
        Task<IResponse<bool>> SendAsync(string receiver, string text);
        Task<IResponse<List<bool>>> SendMultipleAsync(List<string> receiver, string text);
    }
}