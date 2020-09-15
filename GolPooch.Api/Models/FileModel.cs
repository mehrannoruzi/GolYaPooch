namespace GolPooch.Api.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[] FileBytes { get; set; }
    }
}