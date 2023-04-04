using BPJ_U2T.Models.OrderAggregate;

namespace BPJ_U2T.DTOS.OrderAccount
{
    public class OrderPaymentRequest
    {
        public string? ID { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
