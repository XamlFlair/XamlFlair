using System;
using System.Collections.Generic;
using System.Text;

namespace XamlFlair
{
	public enum EasingType
	{
		Linear = 0,
		Back,
		Bounce,
// Circle easing not supported in Uno
#if !__UNO__
		Circle,
#endif
		Cubic,
		Elastic,
		Quadratic,
		Quartic,
		Quintic,
		Sine
	}
}