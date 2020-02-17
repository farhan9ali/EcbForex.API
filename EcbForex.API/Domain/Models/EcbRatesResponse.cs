using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EcbForex.API.Domain.Models
{
    public class EcbRatesResponse
    {
        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rates { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
    }
}
