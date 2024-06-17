using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp4.Converters
{
    class SubtractionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"Object[]: {values}\nType: {targetType}\nObject: {parameter}\nCultureInfo: {culture}");

            if (values != null && values.Length == 2 && values[0] is double && values[1] is double)
            {

                double value1 = (double)values[0];
                double value2 = (double)values[1];
                return value1 - value2;
            }

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
