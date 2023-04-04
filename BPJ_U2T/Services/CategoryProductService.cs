using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using Microsoft.EntityFrameworkCore;

namespace BPJ_U2T.Services
{
    public class CategoryProductService : ICategoryProductService
    {
        private readonly DatabaseContext databaseContext;

        public CategoryProductService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<IEnumerable<CategoryProduct>> GetAll()
        {
            return await databaseContext.CategoryProducts.ToListAsync();
        }

        public async Task<CategoryProduct> GetCategoryProductByID(int Id)
        {
            return await databaseContext.CategoryProducts.FindAsync(Id);
        }
    }
}
