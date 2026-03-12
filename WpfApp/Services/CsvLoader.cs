using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DI;
using WpfApp.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace WpfApp.Services
{
    public class CsvLoader : ICsvLoader
    {
        private readonly string dirProject = Directory.GetParent(
                    Directory.GetParent(
                            Directory.GetParent(Directory.GetCurrentDirectory()).ToString())
                    .ToString())
                .ToString();

        public async Task<List<CsvData>> LoadAsync(string csvPath, CancellationToken ct = default)
        {
            if (!FileNameValidator.IsValidFileName(csvPath))
                csvPath = Path.Combine(dirProject,"Data", "Data.csv");
            using var reader = new StreamReader(csvPath);

            var options = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";",
                MissingFieldFound = null,
                BadDataFound = null,
                HeaderValidated = null
            };
            using var csv = new CsvReader(reader, options);

            var records = new List<CsvData>();
            await foreach (var r in csv.GetRecordsAsync<CsvData>(ct))
            {
                records.Add(r);
            }
            return records;
        }
    }
}
