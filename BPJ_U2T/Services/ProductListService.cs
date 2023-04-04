using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using BPJ_U2T.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace BPJ_U2T.Services
{
    public class ProductListService : IProductListService
    {
        private readonly DatabaseContext databaseContext;
        public ProductListService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<IEnumerable<OrderItem>> GetAll(string idOrder)
        {
            return await databaseContext.OrderItems.Include(e => e.Product).Where(e => e.OrderAccountID == idOrder).ToListAsync();
        }

        public async Task<List<OrderItem>> GetByIdProduct(int idProduct)
        {
            return await databaseContext.OrderItems.Where(e => e.ProductID == idProduct).ToListAsync();    
        }

        public async Task<OrderItem> GetById(string id)
        {
           var result = await databaseContext.OrderItems.Include(e => e.Product).SingleOrDefaultAsync(e => e.ID == id);
            return result;
        }

        public async Task<IEnumerable<OrderItem>> GetProductOrdered(string idOrder, string idAccount)
        {
            var data = await databaseContext.OrderItems.Include(e => e.Product).Where(e => e.OrderAccountID == idOrder).ToListAsync();
            for (var i = 0;i < data.Count(); i++)
            {

            }
            return null;
        }

    }
}
