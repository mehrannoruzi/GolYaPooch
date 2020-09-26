using Microsoft.AspNetCore.Http;

namespace GolPooch.Api.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
    }
}