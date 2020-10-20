using CsvHelper.Configuration.Attributes;

namespace ProductCSVParser.Business.Models.Csv
{
    public class CsvProduct
    {
        [Index(0)]
        public int Id { get; set; }

        [Index(1)]
        public string Name { get; set; }
    }
}