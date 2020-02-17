namespace EcbForex.API.Domain.Models
{
    public class RatesReportRequest
    {
        public string Dates { get; set; }
        public string Base { get; set; }
        public string Target { get; set; }
    }
}