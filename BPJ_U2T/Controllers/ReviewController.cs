using BPJ_U2T.DTOS.Account;
using BPJ_U2T.DTOS.Review;
using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPJ_U2T.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;
        private readonly IReviewImageService reviewImageService;

        public ReviewController(IReviewService reviewService, IReviewImageService reviewImageService)
        {
            this.reviewService = reviewService;
            this.reviewImageService = reviewImageService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetReviewAll(int idProduct)
        {
            var result = (await reviewService.GetAll(idProduct)).Select(ReviewResponse.FromReview);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบข้อมูล" });
            }
            return Ok(new { msg = "OK", data = result });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetReviewByIdProduct(int idProduct)
        {
            var result = (await reviewService.GetByIdProductList(idProduct)).Select(ReviewResponse.FromReview);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบข้อมูล" });
            }
            return Ok(new { msg = "OK", data = result});
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetReviewByIdAccount(int idAccount, int idProduct)
        {
            var result = await reviewService.GetByIdAccount(idAccount, idProduct);
            if (result.Count() == 0)
            {
                return Ok(new { msg = "ไม่พบข้อมูล" });
            }
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddReview([FromForm] ReviewRequest reviewRequest)
        {
            #region จัดการรูปภาพ
            (string erorrImage, List<string> imageName) = await reviewService.UploadImage(reviewRequest.ImageFiles);
            if (!string.IsNullOrEmpty(erorrImage)) return BadRequest(erorrImage);
            #endregion
            var review = reviewRequest.Adapt<Review>();
            var result = await reviewService.Create(review);
            await reviewImageService.Create(imageName, result.ID);
            return Ok(new { msg = "OK", data = review });
        }
    }
}
