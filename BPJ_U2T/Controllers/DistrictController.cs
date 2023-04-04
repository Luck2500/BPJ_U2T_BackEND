using BPJ_U2T.DTOS.Account;
using BPJ_U2T.DTOS.District;
using BPJ_U2T.DTOS.Product;
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
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService districtService;

        public DistrictController(IDistrictService districtService)
        {
            this.districtService = districtService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetDistrictAll()
        {
            return Ok((await districtService.GetAllDistrict()).Select(DistrictResponse.FromDistrict));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetDistrictByID(int id)
        {
            return Ok(await districtService.GetDistrictByID(id));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<District>> AddDistrict([FromForm] DistrictRequest districtRequest)
        {
            
            (string erorrMesage, string imageName) = await districtService.UploadImage(districtRequest.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage)) return BadRequest(erorrMesage);
            var district = districtRequest.Adapt<District>();
            district.Image = imageName;
            await districtService.Create(district);
            return Ok(new { msg = "OK", data = district });
        }
    }
}
