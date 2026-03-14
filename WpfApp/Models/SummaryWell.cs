using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class SummaryWell
    {
        public string WellId { get; set; }
        public double DepthTo { get;  }
        public int NumInterval { get;  }
        public double OveragePorisity { get; }
        public string MostCommonRock { get; }

        public SummaryWell(string wellId,double depthTo, int numInterval, 
            double overagePorisity, string mostCommonRock)
        {
            WellId = wellId;
            DepthTo = depthTo;
            NumInterval = numInterval;
            OveragePorisity = overagePorisity;
            MostCommonRock = mostCommonRock;
        }

        // Средняя пористость по толщине (взвешенная)
        public static double WeightedAveragePorosity(IEnumerable<Interval> intervals)
        {
            if (intervals == null) 
                throw new ArgumentNullException(nameof(intervals));

            double totalThickness = 0.0;
            double weightedSumPorosity = 0.0;

            foreach (var interval in intervals)
            {
                int thickness = interval.DepthTo - interval.DepthFrom;
                if (thickness <= 0) continue;

                totalThickness += thickness;
                weightedSumPorosity += interval.Porosity * thickness;
            }

            return totalThickness > 0 ? weightedSumPorosity / totalThickness : double.NaN;
        }

        // Самая распространённая порода по суммарной толщине
        public static string MostCommonRockByTotalThickness(IEnumerable<Interval> intervals)
        {
            if (intervals == null) 
                throw new ArgumentNullException(nameof(intervals));

            // Группируем по породе, суммируем толщину
            var dict = new Dictionary<string, double>();

            foreach (var interval in intervals)
            {
                int thickness = interval.DepthTo - interval.DepthFrom;
                if (thickness <= 0) continue;
                var rock = interval.Rock;

                if (rock == null) 
                    continue; // пропускаем пустые породы, если нужно учитывать "не прописанные" - можно добавить

                if (dict.TryGetValue(rock.ToString(), out var acc))
                    dict[rock.ToString()] = acc + thickness;
                else
                    dict[rock.ToString()] = thickness;
            }

            if (dict.Count == 0) 
                return "Название породы не указано";

            string mostCommon = string.Empty;
            double maxThickness = double.MinValue;

            foreach (var kvp in dict)
            {
                if (kvp.Value > maxThickness)
                {
                    maxThickness = kvp.Value;
                    mostCommon = kvp.Key;
                }
            }

            return mostCommon.Length==0? "Название породы не указано":mostCommon;
        }
    }
}
