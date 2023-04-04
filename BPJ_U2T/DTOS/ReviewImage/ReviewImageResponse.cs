﻿namespace BPJ_U2T.DTOS.ImageReview
{
    public class ReviewImageResponse
    {
        public string ID { get; set; }
        public string Image { get; set; }
        static public ReviewImageResponse FromReviewImage(Models.ReviewImage reviewImage)
        {
            return new ReviewImageResponse
            {
                ID = reviewImage.ID,
                Image = !string.IsNullOrEmpty(reviewImage.Image) ? "https://localhost:7141/" + "images/" + reviewImage.Image : "" ,

            };
        }
    }
}
