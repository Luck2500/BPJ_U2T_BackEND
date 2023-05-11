using BPJ_U2T.Models;
using BPJ_U2T.Settings;

namespace BPJ_U2T.DTOS.Product
{
    public class ProductResponse
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string? Detailsinfo { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
        public int CategoryProductID { get; set; }
        public string DistrictName { get; set; }
        public int DistrictID { get; set; }


        static public ProductResponse FromProduct(Models.Product product)
        {
            return new ProductResponse
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Detailsinfo = product.Detailsinfo,
                Image = !string.IsNullOrEmpty(product.Image) ? UrlServer.Url + "images/" + product.Image : "",
                CategoryProductID = product.CategoryProduct.Id,
                CategoryName = product.CategoryProduct.Name,
                DistrictID = product.District.ID,
                DistrictName = product.District.Name,
            };
        }
    }
}
