using Elk.Core;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Enum;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace GolPooch.Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Update Profile User with iscomplete=true And purchase a product that is not shown.
        /// at first, must check userid + mobilenumber for authentication. 
        /// if these two fields are ok, then update profile
        /// otherwise signout and return status code 401
        /// </summary>
        /// <param name="userId">userid ( are sent by jwt )</param>
        /// <param name="userDto">user information </param>
        /// <returns>user id</returns>
        Task<IResponse<bool>> UpdateProfileAsync(User user, UserDto userDto);

        Task<IResponse<UserDto>> GetProfileAsync(int userId);

        /// <summary>
        /// Upload awatar and save in host, and update user with awatar address
        /// </summary>
        /// <param name="userId">userid</param>
        /// <param name="photo">binary of photo</param>
        /// <returns>address of photo hosted</returns>
        Task<IResponse<string>> UploadAwatarAsync(int userId, string fileExtension, byte[] fileBytes);

        Task<IResponse<bool>> LogActivityAsync(long mobileNumber, HttpContext httpContext, ActivityLogType type = ActivityLogType.Login);
        Task<IResponse<PagingListDetails<ActivityLog>>> GetActivitiesAsync(long mobileNumber, PagingParameter pagingParameter);
    }
}