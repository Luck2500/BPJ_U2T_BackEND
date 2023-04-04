using BPJ_U2T.DTOS.Cart;
using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPJ_U2T.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartCustomerService cartCustomerService;

        public CartController(ICartCustomerService cartCustomerService)
        {
            this.cartCustomerService = cartCustomerService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCartAll(int idAccount)
        {
            var data = (await cartCustomerService.GetAll(idAccount)).Select(CartResponse.FromCart);
            return Ok(data);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCartByID(string id)
        {
            var data = CartResponse.FromCart(await cartCustomerService.GetByID(id));
            return Ok(data);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCart([FromForm] CartRequest cartRequest)
        {
            var cart = cartRequest.Adapt<Cart>();
            await cartCustomerService.Create(cart);
            return Ok(new { msg = "OK" });
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCart([FromForm] CartRequest cartRequest)
        {
            var cart = await cartCustomerService.GetByID(cartRequest.ID);
            if (cart == null)
            {
                return Ok(new { msg = "ไม่พบสินค้า" });
            }
            var result = cartRequest.Adapt(cart);
            await cartCustomerService.Update(result);
            return Ok(new { msg = "OK" });
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteCart(string id)
        {
            var result = await cartCustomerService.GetByID(id);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบสินค้า" });
            }

            await cartCustomerService.Delete(result);
            return Ok(new { msg = "OK", data = result });
        }
    }
}
