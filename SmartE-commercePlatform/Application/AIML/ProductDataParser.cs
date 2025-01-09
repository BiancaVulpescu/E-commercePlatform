using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
namespace Application.AIML
{
    public class ProductDataParser
    {
        public static List<ProductData> ParseCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            return csv.GetRecords<ProductData>().ToList();
        }
    }
}
