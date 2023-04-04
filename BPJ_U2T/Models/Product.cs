using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPJ_U2T.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Detailsinfo { get; set; }
        public string? Image { get; set; }
        public int DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public virtual District District { get; set; }
        public int CategoryProductID { get; set; }
        [ForeignKey("CategoryProductID")]
        public virtual CategoryProduct CategoryProduct { get; set; }
    }
}
