namespace BPJ_U2T.DTOS.Review
{
    public class ReviewResponse
    {
        public string ID { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Text { get; set; }
        public int AccountID { get; set; }
        public int? ProductID { get; set; }
        public Models.Account Account { get; set; }
        public Models.Product Product { get; set; }
        static public ReviewResponse FromReview(Models.Review review)
        {
            return new ReviewResponse
            {
                ID = review.ID,
                Text = review.Text,
                AccountID = review.AccountID,
                ProductID = review.ProductID,
                Created = review.Created,
                Account = review.Account,
                Product = review.Product,

            };
        }
    }
}
