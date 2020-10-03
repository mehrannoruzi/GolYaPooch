namespace GolPooch.Api.Models
{
    public class PaymentVerifyModel
    {
        public int PaymentTransactionId { get; set; }
        public string Status { get; set; }
        public string Authority { get; set; }
    }
}
