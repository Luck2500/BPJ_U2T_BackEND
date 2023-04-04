using System.ComponentModel.DataAnnotations;

namespace BPJ_U2T.DTOS.Product
{
    public class ProductRequest
    {
        
        public int? ID { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "no more than {1} chars")]
        public string Name { get; set; }

        [Required]
        [Range(0, 1_000_000, ErrorMessage = "between {1}-{2}")]
        public double Price { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "between {1}-{2}")]
        public int Stock { get; set; }

        [Required]
        public int DistrictID { get; set; }
        [Required]
        public int CategoryProductID { get; set; }
        public string Detailsinfo { get; set; }

        public IFormFileCollection? FormFiles { get; set; }
    }
}
