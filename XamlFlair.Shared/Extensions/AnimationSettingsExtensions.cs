#if __WPF__
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
#else
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
#endif

namespace XamlFlair.Extensions
{
	internal static class AnimationSettingsExtensions
	{
#if __WPF__ || __UNO__
		internal static EasingFunctionBase GetEase(this AnimationSettings settings)
		{
			EasingFunctionBase ease;

			switch (settings.Easing)
			{
				case EasingType.Back:
					ease = new BackEase();
					break;

				case EasingType.Bounce:
					ease = new BounceEase();
					break;

#if !__UNO__
				// Circle easing not supported in Uno
				case EasingType.Circle:
					ease = new CircleEase();
					break;
#endif

				case EasingType.Cubic:
					ease = new CubicEase();
					break;

				case EasingType.Elastic:
					ease = new ElasticEase();
					break;

				case EasingType.Linear:
					ease = null;
					break;

				case EasingType.Quadratic:
					ease = new QuadraticEase();
					break;

				case EasingType.Quartic:
					ease = new QuarticEase();
					break;

				case EasingType.Quintic:
					ease = new QuinticEase();
					break;

				case EasingType.Sine:
					ease = new SineEase();
					break;

				default:
					ease = new CubicEase();
					break;
			}

			if (ease != null)
			{
				ease.EasingMode = settings.EasingMode;
			}

			return ease;
		}
#endif

					internal static AnimationSettings ApplyOverrides(this AnimationSettings settings, AnimationSettings other)
		{
			var updated = new AnimationSettings();

			var kind = other.Kind;
			updated.Kind = kind != AnimationSettings.DEFAULT_KIND ? kind : settings.Kind;

			var duration = other.Duration;
			updated.Duration = duration != AnimationSettings.DEFAULT_DURATION ? duration : settings.Duration;

			var delay = other.Delay;
			updated.Delay = delay != 0 ? delay : settings.Delay;

			var opacity = other.Opacity;
			updated.Opacity = opacity != 1 ? opacity : settings.Opacity;

			var offsetX = other.OffsetX;
			updated.OffsetX = offsetX != Offset.Empty ? offsetX : settings.OffsetX;

			var offsetY = other.OffsetY;
			updated.OffsetY = offsetY != Offset.Empty ? offsetY : settings.OffsetY;

			var rotation = other.Rotation;
			updated.Rotation = rotation != 0 ? rotation : settings.Rotation;

			var blur = other.BlurRadius;
			updated.BlurRadius = blur != 0 ? blur : settings.BlurRadius;

			var scaleX = other.ScaleX;
			updated.ScaleX = scaleX != 1 ? scaleX : settings.ScaleX;

			var scaleY = other.ScaleY;
			updated.ScaleY = scaleY != 1 ? scaleY : settings.ScaleY;

			var origin = other.TransformCenterPoint;
			updated.TransformCenterPoint = origin != AnimationSettings.DEFAULT_TRANSFORM_CENTER_POINT ? origin : settings.TransformCenterPoint;

			var easingMode = other.EasingMode;
			updated.EasingMode = easingMode != AnimationSettings.DEFAULT_EASING_MODE ? easingMode : settings.EasingMode;

			var easing = other.Easing;
			updated.Easing = easing != AnimationSettings.DEFAULT_EASING ? easing : settings.Easing;

			var @event = other.Event;
			updated.Event = @event != AnimationSettings.DEFAULT_EVENT ? @event : settings.Event;

#if __UWP__
			var offsetZ = other.OffsetZ;
			updated.OffsetZ = offsetZ != 0 ? offsetZ : settings.OffsetZ;

			var scaleZ = other.ScaleZ;
			updated.ScaleZ = scaleZ != 1 ? scaleZ : settings.ScaleZ;

			var saturation = other.Saturation;
			updated.Saturation = saturation != AnimationSettings.DEFAULT_SATURATION ? saturation : settings.Saturation;

			var tint = other.Tint;
			updated.Tint = tint != AnimationSettings.DEFAULT_TINT ? tint : settings.Tint;
#endif

			return updated;
		}

		internal static List<AnimationSettings> ToSettingsList(this IAnimationSettings settings)
		{
			var animations = new List<AnimationSettings>();

			if (settings is CompoundSettings compound)
			{
				animations.AddRange(compound.Sequence);
			}
			else if (settings is AnimationSettings anim)
			{
				animations.Add(anim);
			}

			return animations;
		}
	}
}