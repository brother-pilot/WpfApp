using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DI;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class SummaryService : ISummaryService
    {
        public List<SummaryWell> BuildSummary(IEnumerable<Well> wells)
        {
            var res= new List<SummaryWell>();
            foreach (var w in wells) 
            {
                res.Add(
                    new SummaryWell(
                        w.WellId,
                        w.Intervals.Max(i=>i.DepthTo),
                        w.Intervals.Count,
                        SummaryWell.WeightedAveragePorosity(w.Intervals),
                        SummaryWell.MostCommonRockByTotalThickness(w.Intervals)
                    ));
            }
            return res;
        }
    }
}
