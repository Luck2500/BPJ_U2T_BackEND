using BPJ_U2T.Models;

namespace BPJ_U2T.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll(string searchName = "", string searchCategory = "",string searchDistrict = "");
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetByID(int? id);
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(Product product);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
    }
}
