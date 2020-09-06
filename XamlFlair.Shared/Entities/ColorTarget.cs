// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
using System;
using System.Collections.Generic;
using System.Text;

namespace XamlFlair
{
	public enum ColorTarget
	{
		Background = 0,
		Foreground,
		BorderBrush,
		Fill,
		Stroke
	}
}
#endif