using System;

namespace GolPooch.Domain.Dto
{
    public class PaymentResultDto
    {
        public string ProductSubject { get; set; }
        public string ProductText { get; set; }
        public string TrackingId { get; set; }
        public string Date { get; set; }
        public DateTime Time { get; set; }
        public int Price { get; set; }
        public int Chance { get; set; }
    }
}