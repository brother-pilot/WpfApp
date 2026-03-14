using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class ValidResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; } = new List<string>();

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var error in Errors)
            {
                builder.Append(error);
            }
            return builder.ToString();
        }
    }
}
