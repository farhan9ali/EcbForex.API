using System;

namespace EcbForex.API.Domain.Models
{
    public class RateModel
    {
        public DateTime? Date { get; set; }
        public decimal Rate { get; set; }
    }
}