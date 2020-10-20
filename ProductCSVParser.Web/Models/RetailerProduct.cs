using System;

namespace ProductCSVParser.Web.Models
{
    public class RetailerProduct
    {
        public int Id { get; set; }

        public string RetailerName { get; set; }

        public string RetailerProductCode { get; set; }

        public string RetailerProductCodeType { get; set; }

        public DateTime DateReceived { get; set; }
    }
}