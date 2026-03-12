using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
   public class CsvData
    {
        public string WellId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int DepthFrom { get; set; }
        public int DepthTo { get; set; }
        public Rock Rock { get; set; }
        public double Porosity { get; set; }
    }
}
