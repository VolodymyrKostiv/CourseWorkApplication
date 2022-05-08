using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CourseWorkApplication.Converters
{
    public class ProductWithQuantityPriceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double a = 0, b = 0;
            if (Double.TryParse(values[0].ToString(), out a) && Double.TryParse(values[1].ToString(), out b))
                return (a * b).ToString();
            return String.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
