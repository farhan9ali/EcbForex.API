using System.Collections.Generic;
using System.Threading.Tasks;
using EcbForex.API.Domain.Models;

namespace EcbForex.API.Domain.Clients
{
    public interface IEcbClient
    {
        Task<List<RateModel>> GetHistoricalRates(RatesReportRequest reportRequest);
    }
}