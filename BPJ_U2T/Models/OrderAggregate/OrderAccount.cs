using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BPJ_U2T.Models.OrderAggregate
{
    public class OrderAccount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.WaitingForPayment;
        public string? ProofOfPayment { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int PriceTotal { get; set; }
        public int DeliveryFee { get; set; } // ค่าจัดส่ง
        public bool AccountStatus { get; set; }
        public int AccountID { get; set; }
        [ForeignKey("AccountID")]
        //[ValidateNever]
        public virtual Account Account { get; set; }
    }
}
