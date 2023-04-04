using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BPJ_U2T.DTOS.ProductDescription;
using BPJ_U2T.Models;

namespace BPJ_U2T.DTOS.ProductDetailProcess
{
    public class ProductDetailProcessResponse
    {
        public string ID { get; set; }
        public string NameRawMaterial { get; set; }// วัตถุดิบ
        public string MakeProductsprocess { get; set; } //วิธีการทำ
        public DateTime CreatedProductDetail { get; set; } = DateTime.Now;
        public string? VideoProduct { get; set; }
        public int? ProductID { get; set; }
        public Models.Product Product { get; set; }

        static public ProductDetailProcessResponse FromProductDetailProcess(Models.ProductDetailProcess productDetailProcess)
        {
            // return ตัวมันเอง
            return new ProductDetailProcessResponse
            {
                ID = productDetailProcess.ID,
                NameRawMaterial = productDetailProcess.NameRawMaterial,
                MakeProductsprocess= productDetailProcess.MakeProductsprocess,
                VideoProduct = !string.IsNullOrEmpty(productDetailProcess.VideoProduct) ? "https://localhost:7141/" + "vedio/" + productDetailProcess.VideoProduct : "",
                ProductID = productDetailProcess.ProductID,
                Product = productDetailProcess.Product,
            };
        }
    }
}
