using BPJ_U2T.Models.OrderAggregate;

namespace BPJ_U2T.Interfaces
{
    public interface IProductListService
    {
        Task<IEnumerable<OrderItem>> GetAll(string idOrder);
        Task<List<OrderItem>> GetByIdProduct(int idProduct);
        Task<OrderItem> GetById(string id);
        // ดึงสินค้าที่ต้องชำระเงินอยู่ในส่วน Seller
        Task<IEnumerable<OrderItem>> GetProductOrdered(string idOrder , string idAccount);
    }
}
