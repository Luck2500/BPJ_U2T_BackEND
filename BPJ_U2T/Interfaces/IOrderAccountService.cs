using BPJ_U2T.DTOS.OrderAccount;
using BPJ_U2T.Models.OrderAggregate;

namespace BPJ_U2T.interfaces
{
    public interface IOrderAccountService
    {
        Task<IEnumerable<OrderDTO>> GetAll(int idAccount);
        Task<OrderAccount> GetByID(string id);
        Task AddOrder(ProductListOrderRequest productListOrderRequest);
        Task<IEnumerable<OrderItem>> GetAllProductList(string idOrder);
        Task UpdateOrder(OrderAccount orderAccount);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
        Task ConfirmOrder(List<OrderAccount> orderAccounts);
        Task<IEnumerable<OrderAccount>> GetConfirm();
        Task<IEnumerable<OrderAccount>> GetConfirmOrderAccount(int idAccount);
    }
}
