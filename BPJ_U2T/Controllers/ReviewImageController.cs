using BPJ_U2T.DTOS.ImageReview;
using BPJ_U2T.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPJ_U2T.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewImageController : ControllerBase
    {
        private readonly IReviewImageService reviewImageService;

        public ReviewImageController(IReviewImageService reviewImageService)
        {
            this.reviewImageService = reviewImageService;
        }
            
        [HttpGet]
        public async Task<IActionResult> GetReviewImageByIdReview(string idReview)
        {
           var result = (await reviewImageService.GetByIdReview(idReview)).Select(ReviewImageResponse.FromReviewImage);
            if (result == null)
            {
                return Ok(new {msg = "ไม่พบข้อมูล"});
            }
            return Ok(new { msg="OK" , data = result });
        }
    }
}
