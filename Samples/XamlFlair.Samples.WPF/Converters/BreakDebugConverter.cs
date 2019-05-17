using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XamlFlair.Samples.WPF.Converters
{
	public class BreakDebugConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}

			return value;
		}
	}
}