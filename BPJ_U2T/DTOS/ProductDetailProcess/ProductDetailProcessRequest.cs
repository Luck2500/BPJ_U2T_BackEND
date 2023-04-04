namespace BPJ_U2T.DTOS.ProductDetailProcess
{
    public class ProductDetailProcessRequest
    {
        public string? ID { get; set; }
        public string NameRawMaterial { get; set; }
        public string MakeProductsprocess { get; set; }
        public IFormFileCollection? VedioFiles { get; set; }
        public int? ProductID { get; set; }
    }
}
