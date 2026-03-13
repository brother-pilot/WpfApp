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
            
                foreach(var interval in well.Intervals) 
                { 
                
                }
            return new ValidResult();
        }

        private ValidResult Validate(Interval item)
        {
            var r = new ValidResult { IsValid = true };

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
            // можно добавить дополнительные проверки
            return r;
        }

    }
}
