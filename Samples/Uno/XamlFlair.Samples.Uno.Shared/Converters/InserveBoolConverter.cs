using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace XamlFlair.Samples.Uno.Converters
{
    public class InserveBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = value != null && System.Convert.ToBoolean(value);

            return !result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var result = value != null && System.Convert.ToBoolean(value);

            return !result;
        }
    }
}
