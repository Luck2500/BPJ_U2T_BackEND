namespace BPJ_U2T.DTOS.OrderAccount
{
    public class OrderItemDTOV2
    {
        public string ID { get; set; }
        public string OrderAccountID { get; set; }
        public int ProductID { get; set; }
        public int ProductPrice { get; set; }
        public int ProductAmount { get; set; }

        public Models.Product Product { get; set; }
    }
}
