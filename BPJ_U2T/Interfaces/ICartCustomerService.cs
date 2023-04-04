using BPJ_U2T.Models;

namespace BPJ_U2T.Interfaces
{
    public interface ICartCustomerService
    {
        Task<IEnumerable<Cart>> GetAll(int idAccount);
        Task<Cart> GetByID(string ID);
        Task Create(Cart cart);
        Task Update(Cart cart);
        Task Delete(Cart cart);
        Task DeleteImage(string fileName);
    }
}
