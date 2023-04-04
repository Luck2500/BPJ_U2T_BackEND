using BPJ_U2T.DTOS.Product;
using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPJ_U2T.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        private readonly IProductService productService;

        public ProductController(DatabaseContext databaseContext, IProductService productService)
        {
            this.databaseContext = databaseContext;
            this.productService = productService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductAllNew()
        {
            var result = (await productService.GetAllProduct()).Select(ProductResponse.FromProduct);
            return Ok(new { data = result });

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProduct(string? searchName = "", string? searchCategory = "", string? searchDistrict = "")
        {
            var result = (await productService.GetAll(searchName, searchCategory,searchDistrict)).Select(ProductResponse.FromProduct);
            return Ok(new { data = result });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            var result = ProductResponse.FromProduct(await productService.GetByID(id));
            if (result == null) return Ok(new { msg = "ไม่พบสินค้า" });
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Product>> AddProduct([FromForm] ProductRequest productRequest)
        {
            
            (string erorrMesage, string imageName) = await productService.UploadImage(productRequest.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage)) return BadRequest(erorrMesage);
            var product = productRequest.Adapt<Product>();
            product.Image = imageName;
            await productService.Create(product);
            return Ok(new { msg = "OK", data = product });
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Product>> UpdateProduct([FromForm] ProductRequest productRequest)
        {
            var result = await productService.GetByID(productRequest.ID);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบสินค้า" });
            }
            #region จัดการรูปภาพ
            (string errorMesage, string imageName) = await productService.UploadImage(productRequest.FormFiles);
            if (!string.IsNullOrEmpty(errorMesage)) return BadRequest(errorMesage);

            if (!string.IsNullOrEmpty(imageName))
            {
                await productService.DeleteImage(result.Image);
                result.Image = imageName;
            }
            #endregion
            var product = productRequest.Adapt<Product>();
            product.Image = imageName;
            if (string.IsNullOrEmpty(imageName))
            {
                product.Image = result.Image;
            }
            await productService.Update(product);
            return Ok(new { msg = "OK", data = product });
        }

        [HttpDelete("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<Product>> DeleteProduct([FromQuery] int id)
        {
            var result = await productService.GetByID(id);
            if (result == null) return Ok(new { msg = "ไม่พบสินค้า" });
            await productService.Delete(result);
            await productService.DeleteImage(result.Image);
            return Ok(new { msg = "OK", data = result });
        }
    }
}
