using BPJ_U2T.DTOS.Product;
using BPJ_U2T.Interfaces;
using BPJ_U2T.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPJ_U2T.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetReport()
        {
            var result = await reportService.SalesStatistics();
            return Ok(new { data = result });

        }
    }
}
