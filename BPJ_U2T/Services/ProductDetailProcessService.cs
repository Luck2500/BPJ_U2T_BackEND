using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using Microsoft.EntityFrameworkCore;

namespace BPJ_U2T.Services
{
    public class ProductDetailProcessService : IProductDetailProcessService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IProductService productService;
        private readonly IUploadFileService uploadFileService;
        public ProductDetailProcessService(DatabaseContext databaseContext, IProductService productService, IUploadFileService uploadFileService)
        {
            this.databaseContext = databaseContext;
            this.productService = productService;
            this.uploadFileService = uploadFileService;
        }
        public async Task Create(ProductDetailProcess productDetailProcess)
        {
            if (string.IsNullOrEmpty(productDetailProcess.ID)) productDetailProcess.ID = await GenerateID();
            await databaseContext.ProductDetailProcesses.AddAsync(productDetailProcess);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<string> GenerateID()
        {
            Random randomNumber = new Random();
            string Id = "";
            // while คือ roobที่ไมมีที่สิ้นสุดจนกว่าเราจะสั่งให้หยุด
            while (true)
            {
                int num = randomNumber.Next(1000000);

                Id = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss") + "-" + num;

                var result = await databaseContext.ProductDetailProcesses.FindAsync(Id);

                if (result == null)
                {
                    break;
                };
            }
            return Id;
        }

        public async Task<IEnumerable<ProductDetailProcess>> GetAll(int idProduct)
        {
            return await databaseContext.ProductDetailProcesses.Include(e => e.Product).Where(a => a.ProductID.Equals(idProduct)).ToListAsync();
        }

        public async Task<ProductDetailProcess> GetById(int? idProduct)
        {
            
            return await databaseContext.ProductDetailProcesses.Include(e => e.Product).AsNoTracking().FirstOrDefaultAsync(x => x.ProductID == idProduct);
            
        }


        public async Task<(string erorrVedio, string vedioName)> UploadVedio(IFormFileCollection formFiles)
        {
            var erorrVedio = string.Empty;
            //var imageName = new List<string>();
            var vedioName = string.Empty;
            if (uploadFileService.IsUpload(formFiles))
            {
                erorrVedio = uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(erorrVedio))
                {
                    vedioName = (await uploadFileService.UploadVedio(formFiles))[0];
                }
            }
            return (erorrVedio, vedioName);
        }


        public async Task UpdateProductDetailProcess(ProductDetailProcess productDetailProcess)
        {
            databaseContext.ProductDetailProcesses.Update(productDetailProcess);
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteVedio(string fileName)
        {
            await uploadFileService.DeleteVedio(fileName);
        }

        public async Task Delete(ProductDetailProcess productDetailProcess)
        {
            databaseContext.Remove(productDetailProcess);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<ProductDetailProcess> GetByID(string ID)
        {
            var result = await databaseContext.ProductDetailProcesses.Include(e => e.Product).AsNoTracking().FirstOrDefaultAsync(e => e.ID == ID);
            if (result == null)
            {
                throw new Exception("ไม่พบสินค้า");
            }
            return result;
        }
    }
}
