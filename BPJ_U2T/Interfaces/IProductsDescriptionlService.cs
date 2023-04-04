using BPJ_U2T.Models;

namespace BPJ_U2T.interfaces
{
    public interface IProductsDescriptionlService
    {
        Task<IEnumerable<ProductDescription>> GetAllByIDProduct(int idProduct);
        Task<ProductDescription> GetByID(string ID);
        Task<(string errorMessage, List<string> imageName)> UploadImage(IFormFileCollection formFiles);
        Task Create(ProductDescription productDescription, List<string> imageName);
        Task Delete(ProductDescription productDescription);
        Task DeleteImage(string fileName);
    }
}
