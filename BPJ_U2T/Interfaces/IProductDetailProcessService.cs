using BPJ_U2T.Models;

namespace BPJ_U2T.Interfaces
{
    public interface IProductDetailProcessService
    {
        Task<IEnumerable<ProductDetailProcess>> GetAll(int idProduct);
        Task<ProductDetailProcess> GetById(int? idProduct);
        Task<ProductDetailProcess> GetByID(string ID);
        Task Create (ProductDetailProcess productDetailProcess);
        Task UpdateProductDetailProcess (ProductDetailProcess productDetailProcess);
        Task Delete(ProductDetailProcess productDetailProcess);
        Task<(string erorrVedio, string vedioName)> UploadVedio(IFormFileCollection formFiles);
        Task DeleteVedio(string fileName);
    }
}
