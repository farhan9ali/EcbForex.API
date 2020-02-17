using System.Linq;
using System.Threading.Tasks;
using EcbForex.API.Domain.Clients;
using EcbForex.API.Domain.Models;
using EcbForex.API.Domain.Services;
using EcbForex.API.Resources;

namespace EcbForex.API.Services
{
    public class RateService : IRateService
    {
        private readonly IEcbClient _ecbClient;

        public RateService(IEcbClient ecbClient)
        {
            _ecbClient = ecbClient;
        }

        public async Task<RatesReportResource> GetReport(RatesReportRequest request)
        {

            var rates = await _ecbClient.GetHistoricalRates(request);
            rates = rates.OrderBy(x => x.Rate).ToList();
            var response = new RatesReportResource
            {
                Report = new Report
                {
                    MinRate = rates.FirstOrDefault(),
                    MaxRate = rates.LastOrDefault(),
                    AvgRate = rates.Average(x => x.Rate)
                },
                //Rates = rates,
                Base = request.Base,
                Target = request.Target,
            };

            return response;
        }

       
    }

}
