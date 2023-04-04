using BPJ_U2T.DTOS.Account;
using BPJ_U2T.DTOS.Product;
using BPJ_U2T.DTOS.ProductDescription;
using BPJ_U2T.DTOS.ProductDetailProcess;
using BPJ_U2T.DTOS.Review;
using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using BPJ_U2T.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPJ_U2T.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductDetailProcessController : ControllerBase
    {
        private readonly IProductDetailProcessService productDetailProcessService;

        public ProductDetailProcessController(IProductDetailProcessService productDetailProcessService)
        {
            this.productDetailProcessService = productDetailProcessService;
        }

        [HttpGet("[action]/{idProduct}")]
        public async Task<IActionResult> GetProductDetailProcessByIDProduct(int? idProduct)
        {
            var result = await productDetailProcessService.GetById(idProduct);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบข้อมูล" });
            }
            return Ok(new { msg = "OK", data = ProductDetailProcessResponse.FromProductDetailProcess(result) });
            
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddProductDetailProcess([FromForm] ProductDetailProcessRequest detailProcessRequest)
        {
            (string erorrVedio, string vedioName) = await productDetailProcessService.UploadVedio(detailProcessRequest.VedioFiles);
            var detailProcess = detailProcessRequest.Adapt<ProductDetailProcess>();
            detailProcess.VideoProduct = vedioName;
            await productDetailProcessService.Create(detailProcess);
            return Ok(new { msg = "OK", data = detailProcess });
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProductDetailProcess(int? idProduct)
        {
            var result = await productDetailProcessService.GetById(idProduct);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await productDetailProcessService.Delete(result);
            await productDetailProcessService.DeleteVedio(result.VideoProduct);
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ProductDetailProcess>> UpdateProductDetailProcess([FromForm] ProductDetailProcessRequest detailProcessRequest)
        {
            var result = await productDetailProcessService.GetById(detailProcessRequest.ProductID);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบข้อมูลสินค้านี้" });
            }
            #region จัดการวิดีโอ
            (string erorrVedio, string vedioName) = await productDetailProcessService.UploadVedio(detailProcessRequest.VedioFiles);
            if (!string.IsNullOrEmpty(erorrVedio)) return BadRequest(erorrVedio);

            if (!string.IsNullOrEmpty(vedioName))
            {
                await productDetailProcessService.DeleteVedio(result.VideoProduct);
                result.VideoProduct = vedioName;
            }
            #endregion
            var detailProcess = detailProcessRequest.Adapt<ProductDetailProcess>();
            detailProcess.VideoProduct = vedioName;
            if (string.IsNullOrEmpty(vedioName))
            {
                detailProcess.VideoProduct = result.VideoProduct;
            }
            await productDetailProcessService.UpdateProductDetailProcess(detailProcess);
            return Ok(new { msg = "OK", data = detailProcess });
        }
    }
}
