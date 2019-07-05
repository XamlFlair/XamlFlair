using System;
using System.Collections.Generic;
using System.Text;

namespace XamlFlair
{
	public enum EventType
	{
		Loaded = 0,
#if __UWP__
		Loading,
#endif
		None,
		Visibility,
		DataContextChanged,
		PointerOver,
		PointerExit,
		GotFocus,
		LostFocus
	}
}