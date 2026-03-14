using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp.Models
{
    public class Error
    {
        public int NumStr { get; }
        public string WellId { get; }
        public string Errors { get;  }

        public Error(int numStr, string wellId, string errors)
        {
            NumStr=numStr;
            WellId=wellId;
            Errors = errors;
        }

    }
}
