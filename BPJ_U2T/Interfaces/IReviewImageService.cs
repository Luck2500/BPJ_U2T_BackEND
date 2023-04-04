using BPJ_U2T.Models;

namespace BPJ_U2T.Interfaces
{
    public interface IReviewImageService
    {
        Task<List<ReviewImage>> GetByIdReview(string idReview);
        Task Create(List<string> imageName , string ReviewID);
    }
}
