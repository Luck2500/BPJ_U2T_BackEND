using BPJ_U2T.Models;

namespace BPJ_U2T.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAll(int idProduct);
        Task<IEnumerable<Review>> GetByIdAccount(int idAccount, int idProduct);
        Task<Review> Create(Review review);
        Task<IEnumerable<Review>> GetByIdProductList(int idProduct);
        Task<(string erorrImage, List<string> imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
    }
}
