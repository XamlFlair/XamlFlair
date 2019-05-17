using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace XamlFlair.Samples.UWP.Converters
{
	internal class BoolToValueConverter : IValueConverter
	{
		public object NullOrFalseValue { get; set; }
		public object TrueValue { get; set; }

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return (value == null || !System.Convert.ToBoolean(value))
				? NullOrFalseValue
				: TrueValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return TrueValue != null    
				? value.Equals(TrueValue)
				: !value.Equals(NullOrFalseValue);
		}
	}
}