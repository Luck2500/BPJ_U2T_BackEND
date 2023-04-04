namespace BPJ_U2T.Models.OrderAggregate
{
    public enum PaymentStatus
    {
        WaitingForPayment, // กำลังรอการชำระเงิน
        PendingApproval, // รอการอนุมัติ
        SuccessfulPayment, // ชำระเงินสำเร็จ
    }
}
