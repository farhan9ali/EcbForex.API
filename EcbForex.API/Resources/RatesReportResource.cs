using EcbForex.API.Domain.Models;

namespace EcbForex.API.Resources
{
    public class Report
    {
        public RateModel MinRate { get; set; }
        public RateModel MaxRate { get; set; }
        public decimal? AvgRate { get; set; }
    }

    public class RatesReportResource
    {
        public string Base { get; set; }
        public string Target { get; set; }
        public Report Report { get; set; }
        //public List<RateModel> Rates { get; set; }
       
    }
}
