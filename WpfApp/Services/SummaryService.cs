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
        List<ResultWell> ISummaryService.BuildSummary(IEnumerable<Interval> items)
        {
            throw new NotImplementedException();
        }
    }
}
