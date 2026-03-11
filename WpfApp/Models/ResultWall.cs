using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class ResultWall
    {
        public double DepthTo { get; set; }
        public int NumInterval { get; set; }
        public double OveragePorisity { get; set; }
        public Rock MostCommonRock { get; set; }
    }
}
