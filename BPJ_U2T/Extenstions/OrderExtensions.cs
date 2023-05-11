using BPJ_U2T.DTOS.OrderAccount;
using BPJ_U2T.Models.OrderAggregate;
using BPJ_U2T.Models;
using Microsoft.EntityFrameworkCore;

namespace BPJ_U2T.Extenstions
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDTO> ProjectOrderToOrderDTO(this IQueryable<OrderAccount> query, DatabaseContext db)
        {

            return query
               .Select(order => new OrderDTO
               {
                   ID = order.ID,
                   PaymentStatus = order.PaymentStatus,
                   Created = order.Created,
                   ProofOfPayment = order.ProofOfPayment,
                   //Created = order.Address.Created,
                   PriceTotal = order.PriceTotal,
                   DeliveryFee = order.DeliveryFee,
                   AccountStatus = order.AccountStatus,
                   AccountID = order.AccountID,
                   Account = order.Account,
                   OrderItems = db.OrderItems.Where(e => e.OrderAccountID == order.ID).Select(item => new OrderItemDTOV2
                   {
                       ID = item.ID,
                       OrderAccountID = item.OrderAccountID,
                       ProductID = item.ProductID,
                       Product = item.Product,
                       ProductPrice = item.ProductPrice,
                       ProductAmount = item.ProductAmount,
                   }).ToList()
               })
               .AsNoTracking()
               ;
        }
    }
}
