using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPJ_U2T.Models
{
    public class ProductDetailProcess
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public string NameRawMaterial { get; set; }// วัตถุดิบ
        public string MakeProductsprocess { get; set; } //วิธีการทำ
        public DateTime CreatedProductDetail { get; set; } = DateTime.Now;
        public string? VideoProduct { get; set; }
        public int? ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
