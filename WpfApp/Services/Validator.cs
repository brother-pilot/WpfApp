using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DI;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class Validator<T> : IValidator<T>
    {
        ValidResult IValidator<T>.Validate(T item)
        {
            throw new NotImplementedException();
        }
    }
}
