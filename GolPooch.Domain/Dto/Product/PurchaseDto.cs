namespace GolPooch.Domain.Dto
{
    public class PurchaseDto
    {
        public int PurchaseId { get; set; }

        public object ProductOffer { get; set; }

        public object PaymentTransaction { get; set; }

        public byte Chance { get; set; }

        public byte UsedChance { get; set; }

        public bool IsReFoundable { get; set; }

        public string ExpireDateSh { get; set; }
        
        public string InsertDateSh { get; set; }

        public string ModifyDateSh { get; set; }
    }
}
