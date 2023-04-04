using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using Microsoft.EntityFrameworkCore;

namespace BPJ_U2T.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IProductService productService;
        private readonly IUploadFileService uploadFileService;

        public ReviewService(DatabaseContext databaseContext, IProductService productService, IUploadFileService uploadFileService)
        {
            this.databaseContext = databaseContext;
            this.productService = productService;
            this.uploadFileService = uploadFileService;
        }
        public async Task<Review> Create(Review review)
        {
            if (string.IsNullOrEmpty(review.ID)) review.ID = await GenerateID();
            await databaseContext.Reviews.AddAsync(review);
            await databaseContext.SaveChangesAsync();
            return review;
        }

        public async Task DeleteImage(string fileName)
        {
            await uploadFileService.DeleteImages(fileName);
        }

        public async Task<IEnumerable<Review>> GetAll(int idProduct)
        {
            var review = new List<Review>();
            var data = await databaseContext.Reviews.Include(e => e.Account).Include(e => e.Product).ToListAsync();
            if (data == null) return null;
            review.AddRange(data);

            return review;
        }

        public async Task<IEnumerable<Review>> GetByIdAccount(int idAccount, int idProduct)
        {
            var review = await databaseContext.Reviews.Where(e => e.AccountID == idAccount && e.ProductID == idProduct).ToListAsync();
            return review;
        }

        public async Task<IEnumerable<Review>> GetByIdProductList(int idProduct)
        {
            var review = await databaseContext.Reviews.Where(e => e.ProductID == idProduct).Include(e => e.Account).ToListAsync();

            return review;
        }

        public async Task<(string erorrImage, List<string> imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var erorrImage = string.Empty;
            //var imageName = new List<string>();
            var imageName = new List<string>();
            if (uploadFileService.IsUpload(formFiles))
            {
                erorrImage = uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(erorrImage))
                {
                    imageName = (await uploadFileService.UploadImages(formFiles));
                }
            }
            return (erorrImage, imageName);
        }

        public Task<(string erorrVedio, string vedioName)> UploadVedio(IFormFileCollection formFiles)
        {
            throw new NotImplementedException();
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

                var result = await databaseContext.Reviews.FindAsync(Id);

                if (result == null)
                {
                    break;
                };
            }
            return Id;
        }
    }
}
