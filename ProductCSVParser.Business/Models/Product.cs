using System;

namespace ProductCSVParser.Business.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CodeType { get; set; }

        public string Code { get; set; }

        public string RetailerName { get; set; }

        public DateTime DateReceived { get; set; }
    }
}