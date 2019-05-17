using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace XamlFlair.Samples.UWP.Converters
{
	public class BreakDebugConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}

			return value;
		}
	}
}