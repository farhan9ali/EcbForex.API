using System.Threading.Tasks;
using EcbForex.API.Domain.Models;
using EcbForex.API.Domain.Services;
using EcbForex.API.Resources;
using Microsoft.AspNetCore.Mvc;

namespace EcbForex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RatesController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RatesController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet("report")]
        public async Task<RatesReportResource> Report([FromQuery] RatesReportRequest request)
        {
            var calculate = await _rateService.GetReport(request);
            return calculate;
        }

    }
}