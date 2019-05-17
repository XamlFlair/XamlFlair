using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XamlFlair.Samples.WPF.Converters
{
	internal class BoolToValueConverter : IValueConverter
	{
		public object NullOrFalseValue { get; set; }
		public object TrueValue { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value == null || !System.Convert.ToBoolean(value, CultureInfo.InvariantCulture))
				? NullOrFalseValue
				: TrueValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return TrueValue != null
				? value.Equals(TrueValue)
				: !value.Equals(NullOrFalseValue);
		}
	}
}