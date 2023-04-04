using Microsoft.EntityFrameworkCore;
using BPJ_U2T.DTOS.OrderAccount;
using BPJ_U2T.interfaces;
using BPJ_U2T.Models;
using BPJ_U2T.Interfaces;
using BPJ_U2T.Models.OrderAggregate;
using Microsoft.IdentityModel.Tokens;
using BPJ_U2T.Extenstions;

namespace BPJ_U2T.Services
{
    public class OrderAccountService : IOrderAccountService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IUploadFileService uploadFileService;
        public OrderAccountService(DatabaseContext databaseContext, IUploadFileService uploadFileService)
        {
            this.databaseContext = databaseContext;
            this.uploadFileService = uploadFileService;
        }
        public async Task<IEnumerable<OrderAccount>> GetConfirmOrderAccount(int idAccount)
        {
            var result = await databaseContext.OrderAccounts.Include(e => e.Account).Where(e => e.AccountID == idAccount).Where(e => e.ProofOfPayment != null && e.PaymentStatus == PaymentStatus.SuccessfulPayment).ToListAsync();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task AddOrder(ProductListOrderRequest productListOrderRequest)
        {
            List<OrderItem> orderItems = new();
            OrderAccount order = new()
            {
                ID = GenerateID(),
                Created = DateTime.Now,
                AccountID = productListOrderRequest.AccountId,
                PriceTotal = 0,
                ProofOfPayment = "",
                AccountStatus = false,
                PaymentStatus = PaymentStatus.WaitingForPayment
            };

            foreach (var item in productListOrderRequest.Items)
            {
                var product = await databaseContext.Products.FirstOrDefaultAsync(e => e.ID == item.ProductID);
                product.Stock -= item.ProductAmount;
                orderItems.Add(new OrderItem
                {
                    ProductAmount = item.ProductAmount,
                    OrderAccountID = order.ID,
                    ProductID = item.ProductID,
                    ProductPrice = item.ProductPrice,
                    ID = GenerateID(),

                });
                if (!string.IsNullOrEmpty(item.ID))
                    databaseContext.Remove(await databaseContext.Cart.FirstOrDefaultAsync(e => e.ID == item.ID));
            }
            var subtotal = orderItems.Sum(item => item.ProductPrice * item.ProductAmount);
            var deliveryFee = subtotal > 5000 ? 0 : 250;
            order.PriceTotal = subtotal;
            order.DeliveryFee = deliveryFee;

            await databaseContext.AddAsync(order);
            await databaseContext.AddRangeAsync(orderItems);



            await databaseContext.SaveChangesAsync();
        }

        private string GenerateID() => Guid.NewGuid().ToString("N");

        public async Task ConfirmOrder(List<OrderAccount> orderAccounts)
        {
            for (var i = 0; i < orderAccounts.Count(); i++)
            {
                orderAccounts[i].PaymentStatus = PaymentStatus.SuccessfulPayment;
                databaseContext.Update(orderAccounts[i]);
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteImage(string fileName)
        {
            await uploadFileService.DeleteImages(fileName);
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(int idAccount)
        {
            return await databaseContext.OrderAccounts.ProjectOrderToOrderDTO(databaseContext).Where(e => e.AccountID == idAccount).ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetAllProductList(string idOrder)
        {
            return await databaseContext.OrderItems.Include(e => e.Product).Where(e => e.OrderAccountID == idOrder).ToListAsync();
        }

        public async Task<OrderAccount> GetByID(string id)
        {
            var result = await databaseContext.OrderAccounts.AsNoTracking().FirstOrDefaultAsync(e => e.ID == id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<IEnumerable<OrderAccount>> GetConfirm()
        {
            var result = await databaseContext.OrderAccounts.Include(e => e.Account).Where(e => e.ProofOfPayment != null && e.PaymentStatus == PaymentStatus.PendingApproval).ToListAsync();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task UpdateOrder(OrderAccount orderAccount)
        {
            databaseContext.Update(orderAccount);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            //var imageName = new List<string>();
            var imageName = string.Empty;
            if (uploadFileService.IsUpload(formFiles))
            {
                errorMessage = uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await uploadFileService.UploadImages(formFiles))[0];
                }
            }
            return (errorMessage, imageName);
        }
        public async Task RemoveCartProduct(ProductListOrderRequest productListOrderRequest)
        {
            //if (productListOrderRequest.ProductID.Length > 0 && productListOrderRequest != null)
            //{
            //    for (var i = 0; i < productListOrderRequest.ProductID.Length; i++)
            //    {
            //        var result = await databaseContext.Cart.AsNoTracking().FirstOrDefaultAsync(e => e.ID == productListOrderRequest.CartID[i]);
            //        databaseContext.Remove(result);
            //    }
            //}

        }

        public async Task RemoveStockProduct(ProductListOrderRequest productListOrderRequest)
        {
            //if (productListOrderRequest.ProductID.Length > 0 && productListOrderRequest != null)
            //{
            //    for (var i = 0; i < productListOrderRequest.ProductID.Length; i++)
            //    {
            //        var result = await databaseContext.Products.AsNoTracking().FirstOrDefaultAsync(e => e.ID == productListOrderRequest.ProductID[i]);
            //        result.Stock -= productListOrderRequest.ProductAmount[i];
            //        databaseContext.Update(result);
            //    }
            //}
        }

        public async Task<string> GenerateIdProductListr()
        {
            Random randomNumber = new Random();
            string IdProductListr = "";
            // while คือ roobที่ไมมีที่สิ้นสุดจนกว่าเราจะสั่งให้หยุด
            while (true)
            {
                int num = randomNumber.Next(1000000);

                IdProductListr = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss") + "-" + num;


                var result = await databaseContext.OrderItems.FindAsync(IdProductListr);

                if (result == null)
                {
                    break;
                };
            }
            return IdProductListr;
        }

        public async Task<string> GenerateIdOrderCustomer()
        {
            Random randomNumber = new Random();
            string IdProductListr = "";
            // while คือ roobที่ไมมีที่สิ้นสุดจนกว่าเราจะสั่งให้หยุด
            while (true)
            {
                int num = randomNumber.Next(1000000);

                IdProductListr = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss") + "-" + num;


                var result = await databaseContext.OrderAccounts.FindAsync(IdProductListr);

                if (result == null)
                {
                    break;
                };
            }
            return IdProductListr;
        }

    }
}
