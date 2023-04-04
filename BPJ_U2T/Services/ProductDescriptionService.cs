using Microsoft.EntityFrameworkCore;
using BPJ_U2T.interfaces;
using BPJ_U2T.Models;
using BPJ_U2T.Interfaces;

namespace BPJ_U2T.Services
{
    public class ProductDescriptionService : IProductsDescriptionlService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IUploadFileService uploadFileService;

        public ProductDescriptionService(DatabaseContext databaseContext, IUploadFileService uploadFileService)
        {
            this.databaseContext = databaseContext;
            this.uploadFileService = uploadFileService;
        }

        public async Task Create(ProductDescription productDescription, List<string> imageName)
        {
            for (var i = 0; i < imageName.Count(); i++)
            {
                productDescription.ID = await GenerateIdProductDescription();
                productDescription.Image = imageName[i];
                await databaseContext.AddAsync(productDescription);
                await databaseContext.SaveChangesAsync();

            }
        }

        public async Task<string> GenerateIdProductDescription()
        {
            Random randomNumber = new Random();
            string IdProductDescription = "";
            // while คือ roobที่ไมมีที่สิ้นสุดจนกว่าเราจะสั่งให้หยุด
            while (true)
            {
                int num = randomNumber.Next(1000000);

                IdProductDescription = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss") + "-" + num;

                var result = await databaseContext.ProductDescriptions.FindAsync(IdProductDescription);

                if (result == null)
                {
                    break;
                };
            }
            return IdProductDescription;
        }

        public async Task DeleteImage(string fileName)
        {
            await uploadFileService.DeleteImages(fileName);
        }

        public async Task<IEnumerable<ProductDescription>> GetAllByIDProduct(int idProduct)
        {
            return await databaseContext.ProductDescriptions.Where(e => e.ProductID == idProduct).ToListAsync();
            //return await databaseContext.OrderAccount.Where(e => e.AccountID == idAccount).ToListAsync();
        }

        public async Task<ProductDescription> GetByID(string ID)
        {
            var result = await databaseContext.ProductDescriptions.Include(e => e.Product).AsNoTracking().FirstOrDefaultAsync(e => e.ID == ID);
            if (result == null) return null;
            return result;
            
        }

        public async Task<(string errorMessage, List<string> imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageName = new List<string>();
            //var imageName = string.Empty;
            if (uploadFileService.IsUpload(formFiles))
            {
                errorMessage = uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await uploadFileService.UploadImages(formFiles));
                }
            }
            return (errorMessage, imageName);
        }

        public async Task Delete(ProductDescription productDescription)
        {
            databaseContext.Remove(productDescription);
            await databaseContext.SaveChangesAsync();
        }

        
    }
}
