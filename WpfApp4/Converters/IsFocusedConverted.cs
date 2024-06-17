using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp4.Converters
{
    class IsFocusedConverted : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                return false;

            if (values.Length == 1)
            {
                if ((bool)values[0] == true)
                {
                    return true;    
                }
            }

            else if(values.Length == 2) 
            {
                Debug.WriteLine($"{values[0]}, {values[1]}");
                if ((bool)values[0] == true || (bool)values[1] == true)
                {
                    return true;
                }
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
