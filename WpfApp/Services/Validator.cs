using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DI;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class Validator: IValidator
    {

        public ValidResult Validate(Well well) 
        {
            var r = new ValidResult { IsValid = true };

            foreach (var interval in well.Intervals) 
                {
                    ValidateInterval(interval, r);
                }
            ValidateIntersectionIntervals(well, r);
            return r;
        }

        private void ValidateIntersectionIntervals(Well well, ValidResult r)
        {
            var intervals=well.Intervals.OrderBy(i => i.DepthFrom).ToList();
            for (var i = 1; i < intervals.Count; i++) 
            {
                if (intervals[i].DepthFrom < intervals[i - 1].DepthTo)
                {
                    r.IsValid = false;
                    r.Errors.Add("Intersection of the intervals");
                    return;
                }
            }
            return;
        }

        private void ValidateInterval(Interval item, ValidResult r)
        {           
            if (item.DepthFrom < 0)
            {
                r.IsValid = false;
                r.Errors.Add("DepthFrom must be >= 0.");
            }
            if (item.DepthTo <= item.DepthFrom)
            {
                r.IsValid = false;
                r.Errors.Add("DepthTo must be greater than DepthFrom.");
            }
            if (item.Porosity < 0 || item.Porosity > 1)
            {
                r.IsValid = false;
                r.Errors.Add("Porosity must be between 0 and 1.");
            }
            if (string.IsNullOrWhiteSpace(item.Rock.ToString()))
            {
                r.IsValid = false;
                r.Errors.Add("Rock is required.");
            }
            return ;
        }

    }
}
