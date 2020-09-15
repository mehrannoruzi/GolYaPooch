namespace GolPooch.Api.Models
{
    public class VerifyCodeModel
    {
        public long   MobileNumber { get; set; }
        public int PinCode { get; set; }
        public int TransactionId { get; set; }
    }
}
