using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace IRiProducts.Business.Services
{
    public class CsvParserService : ICsvParserService
    {
        public IList<TModel> GetRecords<TModel, TCsvMap>(string path)
            where TModel : class
            where TCsvMap : ClassMap
        {
            try
            {
                using (var reader = new StreamReader(path))
                using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csvReader.Configuration.RegisterClassMap<TCsvMap>();

                    // Set to false to allow the first line to be read
                    csvReader.Configuration.HasHeaderRecord = false;
                    return csvReader.GetRecords<TModel>()?.ToList();
                }
            }
            catch (UnauthorizedAccessException e)
            {
                throw new Exception(e.Message);
            }
            catch (FieldValidationException e)
            {
                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}