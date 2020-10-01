using System;

namespace GolPooch.Domain.Dto
{
    public class PaymentResultDto
    {
        public string ProductName { get; set; }
        public string TrackingId { get; set; }
        public DateTime DateTime { get; set; }
        public int Price { get; set; }
    }
}