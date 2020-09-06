using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamlFlair.Extensions;

#if __WPF__
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
#else
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Media.Animation;
#endif

namespace XamlFlair
{
#if __WPF__
	[System.Windows.Markup.MarkupExtensionReturnType(typeof(IAnimationSettings))]
	public class AnimateExtension : System.Windows.Markup.MarkupExtension
#else
	public class Animate : Windows.UI.Xaml.Markup.MarkupExtension
#endif
	{
		/// <summary>
		/// Specifies the base settings to retrieve initial values from
		/// </summary>
		public IAnimationSettings BasedOn { get; set; }

		/// <summary>
		/// Specifies the animation types to include in the composite animation
		/// </summary>
		public AnimationKind Kind { get; set; } = DefaultSettings.Kind;

		/// <summary>
		/// Specifies the duration of the composite animation
		/// </summary>
		public double Duration { get; set; } = DefaultSettings.Duration;

		/// <summary>
		/// Specifies the delay of the composite animation
		/// </summary>
		public double Delay { get; set; }

		/// <summary>
		/// Specifies the target opacity of the composite animation
		/// </summary>
		public double Opacity { get; set; } = 1;

		/// <summary>
		/// Specifies the target x-offset of the composite animation
		/// </summary>
		/// <remarks>
		/// OffsetX must be a double or a star-based value (ex: 150 or 0.75*)
		/// </remarks>
		public Offset OffsetX { get; set; } = Offset.Empty;

		/// <summary>
		/// Specifies the target y-offset of the composite animation
		/// </summary>
		/// <remarks>
		/// OffsetY must be a double or a star-based value (ex: 150 or 0.75*)
		/// </remarks>
		public Offset OffsetY { get; set; } = Offset.Empty;

#if __UWP__
		/// <summary>
		/// Specifies the target z-offset of the composite animation
		/// </summary>
		public double OffsetZ { get; set; }
#endif

		/// <summary>
		/// Specifies the target x-scaling of the composite animation
		/// </summary>
		public double ScaleX { get; set; } = 1;

		/// <summary>
		/// Specifies the target y-scaling of the composite animation
		/// </summary>
		public double ScaleY { get; set; } = 1;

#if __UWP__
		/// <summary>
		/// Specifies the target z-scaling of the composite animation
		/// </summary>
		public double ScaleZ { get; set; } = 1;
#endif

		/// <summary>
		/// Specifies the target rotation (in degrees) of the composite animation
		/// </summary>
		public double Rotation { get; set; }

// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
		/// <summary>
		/// Specifies the target color of the composite animation
		/// </summary>
		public Color Color { get; set; } = DefaultSettings.Color;

		/// <summary>
		/// Specifies the target property for a color animation
		/// </summary>
		public ColorTarget ColorOn { get; set; } = DefaultSettings.ColorOn;
#endif

		// Blur not supported on Uno
#if !HAS_UNO
		/// <summary>
		/// Specifies the blur amount of the composite animation
		/// </summary>
		public double BlurRadius { get; set; }
#endif

#if __UWP__ && !HAS_UNO
		/// <summary>
		/// Specifies the saturation amount of the composite animation
		/// </summary>
		public double Saturation { get; set; } = DefaultSettings.Saturation;

		/// <summary>
		/// Specifies the tint color of the composite animation
		/// </summary>
		public Color Tint { get; set; } = DefaultSettings.Tint;
#endif

		/// <summary>
		/// Specifies the center point of the element's transform
		/// </summary>
		public Point TransformCenterPoint { get; set; } = DefaultSettings.TransformCenterPoint;

#if __WPF__
		/// <summary>
		/// Specifies the transformation type to use (render or layout)
		/// </summary>
		public TransformationType TransformOn { get; set; } = DefaultSettings.TransformOn;
#endif

		/// <summary>
		/// Specifies the event used to trigger the composite animation
		/// </summary>
		/// <remarks>
		/// This property is disregarded for controls based on ListViewBase (UWP) or ListBox (WPF)
		/// </remarks>
		public EventType Event { get; set; } = DefaultSettings.Event;

		/// <summary>
		/// Specifies the easing of the composite animation
		/// </summary>
		public EasingType Easing { get; set; } = DefaultSettings.Easing;

		/// <summary>
		/// Specifies the easing's mode of the composite animation
		/// </summary>
		public EasingMode EasingMode { get; set; } = DefaultSettings.Mode;

		private IAnimationSettings GetAnimationSettings()
		{
			if (BasedOn is CompoundSettings compound)
			{
				// Make sure to capture an override on the Event property (if any)
				if (Event != DefaultSettings.Event)
				{
					compound.Event = Event;
				}

				return compound;
			}

			var current = new AnimationSettings()
			{
				Kind = Kind,
				Duration = Duration,
				Delay = Delay,
				Opacity = Opacity,
				OffsetX = OffsetX,
				OffsetY = OffsetY,
				ScaleX = ScaleX,
				ScaleY = ScaleY,
				Rotation = Rotation,
				// Blur not supported on Uno
#if !HAS_UNO
				BlurRadius = BlurRadius,
#endif
				TransformCenterPoint = TransformCenterPoint,
				Easing = Easing,
				EasingMode = EasingMode,
				Event = Event,
#if __WPF__
				TransformOn = TransformOn,
#endif
#if __UWP__
				OffsetZ = OffsetZ,
				ScaleZ = ScaleZ,
				Saturation = Saturation,
				Tint = Tint,
#endif
// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
				Color = Color,
				ColorOn = ColorOn,
#endif
			};

			// If "BasedOn" is used, return an AnimationSettings
			// object that uses the values in "BasedOn" and then
			// overrides those with updated values from "current"
			return BasedOn == null
				? current
				: ((AnimationSettings)BasedOn)?.ApplyOverrides(current);
		}

#if __WPF__
		public override object ProvideValue(IServiceProvider serviceProvider)
			=> GetAnimationSettings();
#else
		protected override object ProvideValue()
			=> GetAnimationSettings();
#endif
	}
}