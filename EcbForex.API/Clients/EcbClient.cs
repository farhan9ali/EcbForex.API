using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EcbForex.API.Domain.Clients;
using EcbForex.API.Domain.Models;
using EcbForex.API.Domain.Models.Exceptions;
using EcbForex.API.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace EcbForex.API.Clients
{
    public class EcbClient : IEcbClient
    {
        private readonly ILogger<EcbClient> _logger;
        private readonly IRestClient _client;

        public EcbClient(ILogger<EcbClient> logger, IOptions<Settings> options, IRestClient restClient)
        {
            _logger = logger;
            _client = restClient;
            _client.BaseUrl = options.Value.EcbBaseUrl;
        }

        public async Task<List<RateModel>> GetHistoricalRates(RatesReportRequest reportRequest)
        {
            var tasks = new List<Task<IRestResponse<EcbRatesResponse>>>();

            foreach (var date in reportRequest.Dates.Split(",").Distinct())
            {
                var request = new RestRequest(date, Method.GET);
                request.AddQueryParameter("base", reportRequest.Base);
                request.AddQueryParameter("symbols", reportRequest.Target);
                tasks.Add(_client.ExecuteAsync<EcbRatesResponse>(request));
            }
            var response = await Task.WhenAll(tasks);
            //tasks.ForEach(t=> list.Add(HandleResponse(t.Result)));
            return response.Select(HandleResponse).ToList();
        }
       
        private RateModel HandleResponse(IRestResponse<EcbRatesResponse> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new RateModel
                    {
                        Date = response.Request.Resource.ToDateTime(),
                        Rate = response.Data.Rates.First().Value
                    };
                case HttpStatusCode.BadRequest:
                    _logger.Log(LogLevel.Debug,$"Response: {response.StatusCode}, {response.Content}");
                    var errorResponse = JsonConvert.DeserializeObject<EcbErrorResponse>(response.Content) ?? new EcbErrorResponse();
                    throw new BadRequestException(errorResponse.Error);
                default:
                    _logger.Log(LogLevel.Error,$"Response: {response.StatusCode}, {response.Content}");
                    var error = JsonConvert.DeserializeObject<EcbErrorResponse>(response.Content) ?? new EcbErrorResponse();
                    throw new Exception(error.Error);
            }
        }
    }
}
