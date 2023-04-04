namespace BPJ_U2T.DTOS.OrderAccount
{
    public class ProductListOrderRequest
    {
        public int AccountId { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
}
