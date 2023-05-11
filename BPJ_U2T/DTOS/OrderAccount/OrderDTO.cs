using BPJ_U2T.Models.OrderAggregate;

namespace BPJ_U2T.DTOS.OrderAccount
{
    public class OrderDTO
    {
        public string ID { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.WaitingForPayment;
        public string? ProofOfPayment { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int PriceTotal { get; set; }
        public int DeliveryFee { get; set; }
        public bool AccountStatus { get; set; }
        public int AccountID { get; set; }
        public Models.Account Account { get; set; }
        public List<OrderItemDTOV2> OrderItems { get; set; }
    }
}
