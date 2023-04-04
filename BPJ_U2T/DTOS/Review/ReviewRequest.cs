namespace BPJ_U2T.DTOS.Review
{
    public class ReviewRequest
    {
        public int ProductID { get; set; }
        public string Text { get; set; }
        public int AccountID { get; set; }
        public IFormFileCollection? ImageFiles { get; set; }
    }
}
