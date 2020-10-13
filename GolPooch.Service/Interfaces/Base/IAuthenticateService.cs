using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GolPooch.Service.Interfaces
{
    public interface IAuthenticateService
    {
        /// <summary>
        /// Get verification Code + resend code again
        /// </summary>
        /// <param name="mobileNumber">MobileNumber was entered by user</param>
        /// <returns>return VerificationId for Verify</returns>
        Task<IResponse<int>> GetCodeAsync(long mobileNumber);

        /// <summary>
        /// verify last Code and register user with mobilenumber that user entered
        /// </summary>
        /// <param name="AuthenticateId">the verification primaryKey for finding row</param>
        /// <param name="pinCode">the digit code that was entered by </param>
        /// <param name="httpContext">the httpContext of current request </param>
        /// <returns>User Model</returns>
        Task<IResponse<User>> VerifyCodeAsync(int AuthenticateId, int pinCode, HttpContext httpContext);


        Task<IResponse<User>> Authenticate(string refreshToken);
        Task<IResponse<bool>> UpdateRefreshToken(int userId, string refreshToken);
    }
}