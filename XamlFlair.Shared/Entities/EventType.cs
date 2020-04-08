using System;
using System.Collections.Generic;
using System.Text;

namespace XamlFlair
{
	public enum EventType
	{
		Loaded = 0,
		None,
		Visibility,
		DataContextChanged,
		PointerOver,
		PointerExit,
		GotFocus,
		LostFocus
	}
}