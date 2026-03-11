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
        Task<List<Interval>> ICsvLoader.LoadAsync(string csvPath, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
