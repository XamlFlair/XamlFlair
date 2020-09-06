using System;

#if __WPF__
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
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
	internal static class DefaultSettings
	{
		internal const AnimationKind DEFAULT_KIND = AnimationKind.FadeTo;
		internal const double DEFAULT_DURATION = 500;
		internal const double DEFAULT_INTER_ELEMENT_DELAY = 25;
		internal static readonly Point DEFAULT_TRANSFORM_CENTER_POINT = new Point(0.5, 0.5);
		internal const EasingType DEFAULT_EASING = EasingType.Cubic;
		internal const EasingMode DEFAULT_EASING_MODE = EasingMode.EaseOut;
		internal const EventType DEFAULT_EVENT = EventType.Loaded;

// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
		internal static readonly Color DEFAULT_COLOR = Colors.Transparent;
		internal static readonly ColorTarget DEFAULT_COLOR_ON = ColorTarget.Background;
#endif

#if __WPF__
		internal const TransformationType DEFAULT_TRANSFORM_ON = TransformationType.Render;
#endif

#if __UWP__ && !HAS_UNO
		internal const double DEFAULT_SATURATION = 0.5;
		internal static readonly Color DEFAULT_TINT = Colors.Transparent;
#endif

		internal static AnimationKind Kind { get; set; } = DEFAULT_KIND;
		internal static double Duration { get; set; } = DEFAULT_DURATION;
		internal static double InterElementDelay { get; set; } = DEFAULT_INTER_ELEMENT_DELAY;
		internal static Point TransformCenterPoint { get; set; } = DEFAULT_TRANSFORM_CENTER_POINT;
		internal static EasingType Easing { get; set; } = DEFAULT_EASING;
		internal static EasingMode Mode { get; set; } = DEFAULT_EASING_MODE;
		internal static EventType Event { get; set; } = DEFAULT_EVENT;

		// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
		internal static Color Color { get; set; } = DEFAULT_COLOR;
		internal static ColorTarget ColorOn { get; set; } = DEFAULT_COLOR_ON;
#endif

#if __WPF__
		internal static TransformationType TransformOn { get; set; } = DEFAULT_TRANSFORM_ON;
#endif

#if __UWP__ && !HAS_UNO
		internal static double Saturation { get; set; } = 0.5;
		internal static Color Tint { get; set; } = Colors.Transparent;
#endif
	}
}