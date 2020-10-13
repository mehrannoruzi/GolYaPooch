namespace GolPooch.Api.Models
{
    public class AuthenticationModel
    {
        public long MobileNumber { get; set; }
        public int PinCode { get; set; }
        public int TransactionId { get; set; }

        public string RefreshToken { get; set; }
    }
}
