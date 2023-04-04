using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using Microsoft.EntityFrameworkCore;

namespace BPJ_U2T.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IUploadFileService uploadFileService;

        public ProductService(DatabaseContext databaseContext, IUploadFileService uploadFileService)
        {
            this.databaseContext = databaseContext;
            this.uploadFileService = uploadFileService;
        }

        public async Task Create(Product product)
        {
         
            await databaseContext.Products.AddAsync(product);
            await databaseContext.SaveChangesAsync();
        }
        public int GenerateID()
        {
            var result = databaseContext.Products.OrderByDescending(a => a.ID).FirstOrDefault();
            if (result != null)
            {
                int ID = Convert.ToInt32(result.ID);
                return ID + 1;
            }
            return 1;
        }
        public async Task Delete(Product product)
        {
            databaseContext.Remove(product);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await databaseContext.Products.Include(e => e.CategoryProduct).Include(e => e.District).OrderByDescending(a => a.ID).Take(6).ToListAsync();
        }

        public async Task<Product> GetByID(int? id)
        {
            return await databaseContext.Products.Include(e => e.CategoryProduct).Include(e => e.District).AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task Update(Product product)
        {
            databaseContext.Products.Update(product);
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

        public async Task DeleteImage(string fileName)
        {
            await uploadFileService.DeleteImages(fileName);
        }

        public async Task<IEnumerable<Product>> GetAll(string searchName = "", string searchCategory = "", string searchDistrict = "")
        {
            var data = databaseContext.Products.Include(e => e.CategoryProduct).Include(e => e.District).AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                data = data.Where(a => a.Name.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchCategory))
            {
                data = data.Where(a => a.CategoryProduct.Name.Contains(searchCategory));
            }
            if (!string.IsNullOrEmpty(searchDistrict))
            {
                data = data.Where(a => a.District.Name.Contains(searchDistrict));
            }
            return data;
        }
    }
}
