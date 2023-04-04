using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using BPJ_U2T.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPJ_U2T.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryProductController : ControllerBase
    {
        private readonly ICategoryProductService categoryProductService;

        public CategoryProductController(ICategoryProductService categoryProductService)
        {
            this.categoryProductService = categoryProductService;
        }
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoryProduct>> GetCategoryProductAll()
        {
            return await categoryProductService.GetAll();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCategoryProductByID(int id)
        {
            return Ok(await categoryProductService.GetCategoryProductByID(id));
        }
    }
}
