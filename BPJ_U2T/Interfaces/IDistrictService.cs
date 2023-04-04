using BPJ_U2T.Models;

namespace BPJ_U2T.Interfaces
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> GetAllDistrict();
        Task<District> GetDistrictByID(int Id);
        Task Create(District district);
        Task Update(District district);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
    }
}
