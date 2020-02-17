using System.Threading.Tasks;
using EcbForex.API.Domain.Models;
using EcbForex.API.Resources;

namespace EcbForex.API.Domain.Services
{
    public interface IRateService
    {
        Task<RatesReportResource> GetReport(RatesReportRequest request);
    }
}