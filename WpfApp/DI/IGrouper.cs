using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.DI
{
    internal interface IGrouper
    {
        List<Well> GroupeWell(IEnumerable<CsvData> items);
    }
}
