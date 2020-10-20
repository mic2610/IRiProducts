using System;
using CsvHelper.Configuration.Attributes;

namespace ProductCSVParser.Business.Models.Csv
{
    public class CsvRetailerProduct
    {
        [Index(0)]
        public int Id { get; set; }

        [Index(1)]
        public string RetailerName { get; set; }

        [Index(2)]
        public string RetailerProductCode { get; set; }

        [Index(3)]
        public string RetailerProductCodeType { get; set; }

        [Index(4)]
        public DateTime DateReceived { get; set; }
    }
}