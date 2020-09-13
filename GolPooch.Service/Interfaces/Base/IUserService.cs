using Elk.Core;
using GolPooch.Domain.Dto;
using System.Threading.Tasks;

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
        Task<IResponse<int>> UpdateProfileAsync(int userId, UserDto userDto);

        /// <summary>
        /// Upload awatar and save in host, and update user with awatar address
        /// </summary>
        /// <param name="userId">userid</param>
        /// <param name="photo">binary of photo</param>
        /// <returns>address of photo hosted</returns>
        Task<IResponse<string>> UploadAwatarAsync(int userId, string fileExtension, byte[] fileBytes);
    }
}