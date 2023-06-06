using BPJ_U2T.DTOS.Report;
using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using BPJ_U2T.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace BPJ_U2T.Services
{
    public class ReportService : IReportService
    {
        private readonly DatabaseContext databaseContext;
        public ReportService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        
        public async Task<SalesStatisticsDTO> SalesStatistics(int? date)
        {
            SalesStatisticsDTO salesStatisticsDTO = new();
            List<OrderItem> orderItems = new();
            var orders = await databaseContext.OrderAccounts.Where(e => 
            e.ProofOfPayment != null && e.PaymentStatus == PaymentStatus.SuccessfulPayment  &&
            e.Created.Year == (date == 0 || date == null ? DateTime.Now.Year : date)
            ).ToListAsync();
            foreach (var order in orders)
            {
                var items = databaseContext.OrderItems
                    .Include(e => e.Product).ThenInclude(e => e.District).Where(e => e.OrderAccountID.Equals(order.ID)).ToList();
                if (items?.Count() > 0)
                {
                    foreach (var item in items)
                    {
                       var check = salesStatisticsDTO.Sales.FirstOrDefault(e => e.DistrictId == item.Product.DistrictID);
                       if (check != null)
                       {
                            check.Price += (item.ProductPrice * item.ProductAmount);
                        }
                        else
                        {
                            salesStatisticsDTO.Sales.Add(new SalesStatisticeItemDTO { Price = (item.ProductPrice * item.ProductAmount), DistrictId = item.Product.District.ID, DistrictName = item.Product.District.Name, });
                        }
                    }
                }
            };

            salesStatisticsDTO.TotalPrice = salesStatisticsDTO.Sales.Sum(e => e.Price);
            foreach (var item in salesStatisticsDTO.Sales)
            {
                var Percen = item.Price * 100 / salesStatisticsDTO.TotalPrice;
                item.Percent = Percen;
            }
            salesStatisticsDTO.Sales = salesStatisticsDTO.Sales.OrderByDescending(e => e.Price).ToList();

            return salesStatisticsDTO;
        }
    }
}
