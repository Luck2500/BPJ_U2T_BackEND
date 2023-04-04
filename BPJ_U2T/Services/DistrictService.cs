using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using Microsoft.EntityFrameworkCore;

namespace BPJ_U2T.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IUploadFileService uploadFileService;

        public DistrictService(DatabaseContext databaseContext, IUploadFileService uploadFileService)
        {
            this.databaseContext = databaseContext;
            this.uploadFileService = uploadFileService;
        }

        public async Task Create(District district)
        {
            await databaseContext.Districts.AddAsync(district);
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteImage(string fileName)
        {
            await uploadFileService.DeleteImages(fileName);
        }

        public async Task<IEnumerable<District>> GetAllDistrict()
        {
            return await databaseContext.Districts.ToListAsync();
        }

        public async Task<District> GetDistrictByID(int Id)
        {
            return await databaseContext.Districts.FindAsync(Id);
        }

        public async Task Update(District district)
        {
            databaseContext.Districts.Update(district);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageName = string.Empty;

            if (uploadFileService.IsUpload(formFiles))
            {
                errorMessage = uploadFileService.Validation(formFiles);
                if (String.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await uploadFileService.UploadImages(formFiles))[0];
                }
            }
            return (errorMessage, imageName);
        }
    }
}
