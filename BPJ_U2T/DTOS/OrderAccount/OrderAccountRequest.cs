namespace BPJ_U2T.DTOS.OrderAccount
{
    public class OrderAccountRequest
    {
        public string? ID { get; set; }
        public bool PaymentStatus { get; set; }
        public string? ProofOfPayment { get; set; }
        public int PriceTotal { get; set; }
        public bool? AccountStatus { get; set; }
        public string AccountID { get; set; }
    }
}
