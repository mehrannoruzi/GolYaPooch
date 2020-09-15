using Elk.Core;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace GolPooch.CrossCutting
{
    public static class HttpFileTools
    {
        public static FilePathModel GetPath(string fileNameWithExtension, string root = "~",
            bool includeDayInPath = false, string objectId = null,
            string urlPrefix = null, string fileNamePrefix = null)
        {
            var persianDate = PersianDateTime.Now;

            #region Create Directory Address
            var path = string.Join("/", root, persianDate.Year, persianDate.Month);
            if (includeDayInPath) path = path + "/" + persianDate.Day;
            path += (objectId == null ? string.Empty : ("/" + objectId));
            var directoryAddress = Path.Combine(Directory.GetCurrentDirectory(), urlPrefix ?? "", "wwwroot", path.Replace("/", "\\"));
            #endregion

            #region Create File Name
            var trustedFileName = WebUtility.HtmlEncode(fileNameWithExtension);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(trustedFileName);
            var fileName = fileNamePrefix != null ? fileNamePrefix + "_" : string.Empty;
            fileName += fileNameWithoutExtension + "_" + persianDate.Ticks.ToString() + Path.GetExtension(trustedFileName);
            #endregion

            if (!FileOperation.CreateDirectory(directoryAddress)) return null;
            return new FilePathModel
            {
                FileName = fileName,
                FullPath = directoryAddress + "\\" + fileName,
                RelativePath = "wwwroot/" + path + "/" + fileName
            };
        }

        public static string Save(IFormFile file, string fullPath)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                if (File.Exists(fullPath)) return fullPath;
            }

            return null;
        }

        public static string Save(byte[] fileBytes, string fullPath)
        {
            if (fileBytes != null && fileBytes.Length > 0)
            {
                File.WriteAllBytes(fullPath, fileBytes);

                if (File.Exists(fullPath)) return fullPath;
            }

            return null;
        }
    }
}