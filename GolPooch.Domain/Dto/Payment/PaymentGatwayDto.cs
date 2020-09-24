using GolPooch.Domain.Enum;

namespace GolPooch.Domain.Dto
{
    public  class PaymentGatwayDto
    {
        public int PaymentGatewayId { get; set; }

        public BankNames BankName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}