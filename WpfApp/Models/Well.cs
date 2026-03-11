using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class Well
    {
        public string WellId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public List<Interval> Intervals { get; set; }
    }
}
