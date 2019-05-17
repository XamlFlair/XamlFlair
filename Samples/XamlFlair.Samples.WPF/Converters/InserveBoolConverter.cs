using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XamlFlair.Samples.WPF.Converters
{
    public class InserveBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value != null && System.Convert.ToBoolean(value);

            return !result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value != null && System.Convert.ToBoolean(value);

            return !result;
        }
    }
}
