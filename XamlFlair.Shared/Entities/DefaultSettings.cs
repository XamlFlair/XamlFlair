using System;

#if __WPF__
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media.Animation;
#else
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
#endif

namespace XamlFlair
{
	public static class DefaultSettings
	{
		public static AnimationKind Kind { get; set; } = AnimationKind.FadeTo;
		public static double Duration { get; set; } = 500;
		public static double InterElementDelay { get; set; } = 25;
		public static Point TransformCenterPoint { get; set; } = new Point(0.5, 0.5);
		public static EasingType Easing { get; set; } = EasingType.Cubic;
		public static EasingMode Mode { get; set; } = EasingMode.EaseOut;
		public static EventType Event { get; set; } = EventType.Loaded;

#if __WPF__
		public static TransformationType Transform { get; set; } = TransformationType.Render;
#endif

#if __UWP__
		public static double Saturation { get; set; } = 0.5;
		public static Color Tint { get; set; } = Colors.Transparent;
#endif
	}
}