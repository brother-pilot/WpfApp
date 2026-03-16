using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfApp.DI;
using WpfApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp.Services
{
    public class JsonSaver: IFileSaver
    {
        public string PathModel { get; }

        public JsonSaver()
        {
            PathModel = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public bool SaveFile(IEnumerable<SummaryWell> data)
        {
            var filePath = Path.Combine(PathModel, "summary.json");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // удобочитаемый формат
            };

            string json = JsonSerializer.Serialize(data, options);

            try
            {
                // Записываем в файл (UTF-8 по умолчанию для WriteAllText)
                File.WriteAllText(filePath, json, Encoding.UTF8);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось сохранить данные в файл!");
                return false;
            } 
        }
    }
}
