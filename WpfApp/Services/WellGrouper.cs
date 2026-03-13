using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DI;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class WellGrouper : IGrouper
    {
        public List<Well> GroupeWell(IEnumerable<CsvData> items)
        {
            var wells = items
            .GroupBy(r => r.WellId)
                .Select(g =>
                {
                    var intervals = g.Select(r => new Interval
                    {
                        DepthFrom = r.DepthFrom,
                        DepthTo = r.DepthTo,
                        Rock = r.Rock ?? new Rock(), // если Rock может быть null, подставляем пустой Rock
                        Porosity = r.Porosity
                    }).ToList();            
                return new Well
                {
                    WellId = g.Key,
                    X = g.Select(gg=>gg.X).First(),
                    Y = g.Select(gg => gg.Y).First(),
                    Intervals = intervals
                };
        })
                .ToList();
            return wells;
        }
    }
}
