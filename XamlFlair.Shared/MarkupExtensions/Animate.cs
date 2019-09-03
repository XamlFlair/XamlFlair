using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if __WPF__
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using XamlFlair.Extensions;
#else
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Animation;
using XamlFlair.Extensions;
#endif

namespace XamlFlair
{
#if __WPF__
	[MarkupExtensionReturnType(typeof(IAnimationSettings))]
	public class AnimateExtension : MarkupExtension
#else
	[MarkupExtensionReturnType(ReturnType = typeof(IAnimationSettings))]
	public class Animate : MarkupExtension
#endif
	{
		/// <summary>
		/// Specifies the base settings to retrieve initial values from
		/// </summary>
		public IAnimationSettings BasedOn { get; set; }

		/// <summary>
		/// Specifies the animation types to include in the composite animation
		/// </summary>
		public AnimationKind Kind { get; set; } = AnimationSettings.DEFAULT_KIND;

		/// <summary>
		/// Specifies the duration of the composite animation
		/// </summary>
		public double Duration { get; set; } = AnimationSettings.DEFAULT_DURATION;

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
		public string OffsetX { get; set; } = AnimationSettings.DEFAULT_TRANSLATION;

		/// <summary>
		/// Specifies the target y-offset of the composite animation
		/// </summary>
		public string OffsetY { get; set; } = AnimationSettings.DEFAULT_TRANSLATION;

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

		/// <summary>
		/// Specifies the blur amount of the composite animation
		/// </summary>
		public double BlurRadius { get; set; }

#if __UWP__
		/// <summary>
		/// Specifies the saturation amount of the composite animation
		/// </summary>
		public double Saturation { get; set; } = AnimationSettings.DEFAULT_SATURATION;

		/// <summary>
		/// Specifies the tint color of the composite animation
		/// </summary>
		public Color Tint { get; set; } = AnimationSettings.DEFAULT_TINT;
#endif

		/// <summary>
		/// Specifies the center point of the element's transform
		/// </summary>
		public Point TransformCenterPoint { get; set; } = AnimationSettings.DEFAULT_TRANSFORM_CENTER_POINT;

		/// <summary>
		/// Specifies the event used to trigger the composite animation
		/// </summary>
		/// <remarks>
		/// This property is disregarded for controls based on ListViewBase (UWP) or ListBox (WPF)
		/// </remarks>
		public EventType Event { get; set; } = AnimationSettings.DEFAULT_EVENT;

		/// <summary>
		/// Specifies the easing of the composite animation
		/// </summary>
		public EasingType Easing { get; set; } = AnimationSettings.DEFAULT_EASING;

		/// <summary>
		/// Specifies the easing's mode of the composite animation
		/// </summary>
		public EasingMode EasingMode { get; set; } = AnimationSettings.DEFAULT_EASING_MODE;

		private IAnimationSettings GetAnimationSettings()
		{
			if (BasedOn is CompoundSettings compound)
			{
				// Make sure to capture an override on the Event property (if any)
				if (Event != AnimationSettings.DEFAULT_EVENT)
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
				BlurRadius = BlurRadius,
				TransformCenterPoint = TransformCenterPoint,
				Easing = Easing,
				EasingMode = EasingMode,
				Event = Event,
#if __UWP__
				OffsetZ = OffsetZ,
				ScaleZ = ScaleZ,
				Saturation = Saturation,
				Tint = Tint,
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