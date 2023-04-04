using BPJ_U2T.Models;

namespace BPJ_U2T.Interfaces
{
    public interface ICategoryProductService
    {
        Task<IEnumerable<CategoryProduct>> GetAll();
        Task<CategoryProduct> GetCategoryProductByID(int Id);
    }
}
