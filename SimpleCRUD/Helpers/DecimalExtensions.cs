using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCRUD.Helpers
{
    public static class DecimalExtensions
    {
        public static int DecimalPartCount(this decimal value)
        {
            return value.ToString(System.Globalization.CultureInfo.InvariantCulture)
                .SkipWhile(c => c != '.')
                .Skip(1)
                .Count();
        }
    }
}
