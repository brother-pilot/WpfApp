using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.DI
{
    internal interface ISummaryService
    {
        List<SummaryWell> BuildSummary(IEnumerable<Well> items);
    }
}
