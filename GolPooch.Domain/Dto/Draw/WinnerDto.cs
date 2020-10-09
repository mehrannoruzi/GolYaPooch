using Elk.Core;

namespace GolPooch.Domain.Dto
{
    public class WinnerDto
    {
        public long Number { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public int ChanceCount { get; set; }
        public int WinCount { get; set; }
    }
}