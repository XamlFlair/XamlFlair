using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using XamlFlair.UWP.Logging;
using static XamlFlair.Constants;

namespace XamlFlair.Extensions
{
	internal static class AnimationExtensions
	{
		// ---------------------------------------------------------------
		// TODO: Investigate whether or not we should be specifying values 
		// for the "z" on Vector3 (ex: visual.Offset.Z instead of 0f)
		// ---------------------------------------------------------------

		internal static void FadeTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<FadeAnimation>(
						element,
						settings,
						to: settings.Opacity));
		}

		internal static void FadeFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateScalarAnimation<FadeAnimation>(
						element,
						settings,
						to: settings.Opacity,
						duration: 1,
						isFrom: true);

					return animGroup.CreateScalarAnimation<FadeAnimation>(
						element,
						settings,
						to: 1);
				});
		}

		internal static void TranslateXTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<TranslateXAnimation>(
						element,
						settings,
						to: (float)settings.OffsetX));
		}

		internal static void TranslateYTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<TranslateYAnimation>(
						element,
						settings,
						to: (float)settings.OffsetY));
		}

		internal static void TranslateZTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<TranslateZAnimation>(
						element,
						settings,
						to: (float)settings.OffsetZ));
		}

		internal static void TranslateXFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateScalarAnimation<TranslateXAnimation>(
						element,
						settings,
						to: (float)settings.OffsetX,
						duration: 1,
						isFrom: true);

					return animGroup.CreateScalarAnimation<TranslateXAnimation>(
						element,
						settings,
						to: 0f);
				});
		}

		internal static void TranslateYFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateScalarAnimation<TranslateYAnimation>(
						element,
						settings,
						to: (float)settings.OffsetY,
						duration: 1,
						isFrom: true);

					return animGroup.CreateScalarAnimation<TranslateYAnimation>(
						element,
						settings,
						to: 0f);
				});
		}

		internal static void TranslateZFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateScalarAnimation<TranslateZAnimation>(
						element,
						settings,
						to: (float)settings.OffsetZ,
						duration: 1,
						isFrom: true);

					return animGroup.CreateScalarAnimation<TranslateZAnimation>(
						element,
						settings,
						to: 0f);
				});
		}

		internal static void ScaleXTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<ScaleXAnimation>(
						element,
						settings,
						to: (float)settings.ScaleX));
		}

		internal static void ScaleYTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<ScaleYAnimation>(
						element,
						settings,
						to: (float)settings.ScaleY));
		}

		internal static void ScaleZTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<ScaleZAnimation>(
						element,
						settings,
						to: (float)settings.ScaleZ));
		}

		internal static void ScaleXFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateScalarAnimation<ScaleXAnimation>(
						element,
						settings,
						to: (float)settings.ScaleX,
						duration: 1,
						isFrom: true);

					return animGroup.CreateScalarAnimation<ScaleXAnimation>(
						element,
						settings,
						to: 1f);
				});
		}

		internal static void ScaleYFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateScalarAnimation<ScaleYAnimation>(
						element,
						settings,
						to: (float)settings.ScaleY,
						duration: 1,
						isFrom: true);

					return animGroup.CreateScalarAnimation<ScaleYAnimation>(
						element,
						settings,
						to: 1f);
				});
		}

		internal static void ScaleZFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateScalarAnimation<ScaleZAnimation>(
						element,
						settings,
						to: (float)settings.ScaleZ,
						duration: 1,
						isFrom: true);

					return animGroup.CreateScalarAnimation<ScaleZAnimation>(
						element,
						settings,
						to: 1f);
				});
		}

		internal static void RotateTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<RotateAnimation>(
						element,
						settings,
						to: settings.Rotation));
		}

		internal static void RotateFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateScalarAnimation<RotateAnimation>(
						element,
						settings,
						to: settings.Rotation,
						duration: 1,
						isFrom: true);

					return animGroup.CreateScalarAnimation<RotateAnimation>(
						element,
						settings,
						to: 0);
				});
		}

		internal static void BlurTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateEffectAnimation<BlurAnimation, double>(
						element,
						settings,
						to: settings.BlurRadius));
		}

		internal static void BlurFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateEffectAnimation<BlurAnimation, double>(
						element,
						settings,
						to: settings.BlurRadius,
						duration: 1,
						isFrom: true);

					return animGroup.CreateEffectAnimation<BlurAnimation, double>(
						element,
						settings,
						to: 0);
				});
		}

		internal static void SaturateTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateEffectAnimation<SaturateAnimation, double>(
						element,
						settings,
						to: settings.Saturation));
		}

		internal static void SaturateFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateEffectAnimation<SaturateAnimation, double>(
						element,
						settings,
						to: settings.Saturation,
						duration: 1,
						isFrom: true);

					return animGroup.CreateEffectAnimation<SaturateAnimation, double>(
						element,
						settings,
						to: AnimationSettings.DEFAULT_SATURATION);
				});
		}

		internal static void TintTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateEffectAnimation<TintAnimation, Color>(
						element,
						settings,
						to: settings.Tint));
		}

		internal static void TintFrom(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
				{
					animGroup.CreateEffectAnimation<TintAnimation, Color>(
						element,
						settings,
						to: settings.Tint,
						duration: 1,
						isFrom: true);

					return animGroup.CreateEffectAnimation<TintAnimation, Color>(
						element,
						settings,
						to: AnimationSettings.DEFAULT_TINT);
				});
		}

		internal static void ApplyInitialSettings(this FrameworkElement element, AnimationSettings settings)
		{
			var group = new AnimationGroup();
			var visual = ElementCompositionPreview.GetElementVisual(element);

			if (settings.Opacity != 1)
			{
				visual.Opacity = (float)settings.Opacity;
			}

			if (settings.OffsetX != 0 || settings.OffsetY != 0)
			{
				visual.Properties.InsertVector3(TargetProperties.Translation, new Vector3((float)settings.OffsetX, (float)settings.OffsetY, (float)settings.OffsetZ));
			}

			if (settings.Rotation != 0)
			{
				visual.RotationAngleInDegrees = (float)settings.Rotation;
			}

			if (settings.ScaleX != 1 || settings.ScaleY != 1 || settings.ScaleZ != 1)
			{
				visual.Scale = new Vector3((float)settings.ScaleX, (float)settings.ScaleY, (float)settings.ScaleZ);
			}

			if (settings.BlurRadius != 0 || settings.Saturation != AnimationSettings.DEFAULT_SATURATION || settings.Tint != AnimationSettings.DEFAULT_TINT)
			{
				var initialSettings = new AnimationSettings()
				{
					BlurRadius = settings.BlurRadius,
					Saturation = settings.Saturation,
					Tint = settings.Tint,
					Duration = 1
				};

				if (settings.BlurRadius != 0)
				{
					BlurTo(element, initialSettings, ref group);
				}

				if (settings.Saturation != AnimationSettings.DEFAULT_SATURATION)
				{
					SaturateTo(element, initialSettings, ref group);
				}

				if (settings.Tint != AnimationSettings.DEFAULT_TINT)
				{
					TintTo(element, initialSettings, ref group);
				}
			}

			group.Begin();
		}

		private static T CreateScalarAnimation<T>(this AnimationGroup group, FrameworkElement element, AnimationSettings settings, double to = 1, double duration = AnimationSettings.DEFAULT_DURATION, bool isFrom = false)
			where T : ScalarAnimationBase, new()
		{
			var animation = new T()
			{
				To = to,
				Duration = duration,
				Settings = settings
			};

			group.Add(element, animation, isFrom);

			return animation;
		}

		// TODO: If not used anymore, remove it (or simply keep for any future need?)
		private static T CreateVectorAnimation<T>(this AnimationGroup group, FrameworkElement element, AnimationSettings settings, Vector3 to, double duration = AnimationSettings.DEFAULT_DURATION, bool isFrom = false)
			where T : VectorAnimationBase, new()
		{
			var animation = new T()
			{
				To = to,
				Duration = duration,
				Settings = settings
			};

			group.Add(element, animation, isFrom);

			return animation;
		}

		private static TAnimation CreateEffectAnimation<TAnimation, TValue>(this AnimationGroup group, FrameworkElement element, AnimationSettings settings, TValue to, double duration = AnimationSettings.DEFAULT_DURATION, bool isFrom = false)
			where TAnimation : EffectAnimationBase<TValue>, new()
		{
			var animation = new TAnimation()
			{
				To = to,
				Duration = duration,
				Settings = settings
			};

			group.Add(element, animation, isFrom);

			return animation;
		}

		private static T CreateExpressionAnimation<T>(this AnimationGroup group, FrameworkElement element, AnimationSettings settings, string expression, bool isFrom = false)
			where T : ExpressionAnimationBase, new()
		{
			var animation = new T()
			{
				Expression = expression,
				Settings = settings
			};

			group.Add(element, animation, isFrom);

			return animation;
		}

		// TODO: Not yet used... Attempt to use in order to specify the "from" values
		// instead of animating the "from" values with a 1 millisecond animation
		private static void SetFromVector3(this FrameworkElement element, string targetProperty, Func<Vector3> initialPropertyVectorFunc, Func<Vector3, Vector3> updatePropertyVectorFunc)
		{
			var props = ElementCompositionPreview.GetElementVisual(element).Properties;

			if (props.TryGetVector3(targetProperty, out var vector) == CompositionGetValueStatus.Succeeded)
			{
				props.InsertVector3(targetProperty, updatePropertyVectorFunc(vector));
			}
			else
			{
				props.InsertVector3(targetProperty, initialPropertyVectorFunc());
			}
		}

		// TODO: Not yet used... Attempt to use in order to specify the "from" values
		// instead of animating the "from" values with a 1 millisecond animation
		private static void SetFromScalar(this FrameworkElement element, string targetProperty, Func<float> initialPropertyScalarFunc, Func<float> updatePropertyScalarFunc)
		{
			var props = ElementCompositionPreview.GetElementVisual(element).Properties;

			if (props.TryGetScalar(targetProperty, out var scalar) == CompositionGetValueStatus.Succeeded)
			{
				props.InsertScalar(targetProperty, updatePropertyScalarFunc());
			}
			else
			{
				props.InsertScalar(targetProperty, initialPropertyScalarFunc());
			}
		}

		internal static void LogAnimationInfo(this FrameworkElement element, AnimationSettings settings, string targetProperty)
		{
			// Build the "element" output with Name + Type if Name exists, else just the Type
			var name = element.Name ?? string.Empty;
			var elementOutput = !string.IsNullOrEmpty(name)
				? $"{name} ({element.GetType()})"
				: element.GetType().ToString();

			var output =
				"\n---------- ANIMATION ----------\n" +
				$"	Timestamp = {DateTimeOffset.Now.ToString("HH:mm:ss:fffff")} \n" +
				$"	Element = {elementOutput} \n" +
				$"	Kind = {settings.Kind} \n" +
				$"	TargetProperty = {targetProperty} \n" +
				$"	Duration = {settings.Duration} \n" +
				$"	Delay = {settings.Delay} \n" +
				$"	Opacity = {settings.Opacity} \n" +
				$"	OffsetX = {settings.OffsetX} \n" +
				$"	OffsetY = {settings.OffsetY} \n" +
				$"	OffsetZ = {settings.OffsetZ} \n" +
				$"	ScaleX = {settings.ScaleX} \n" +
				$"	ScaleY = {settings.ScaleY} \n" +
				$"	ScaleZ = {settings.ScaleZ} \n" +
				$"	Rotation = {settings.Rotation} \n" +
				$"	Blur = {settings.BlurRadius} \n" +
#if __UWP__
				$"	Saturation = {settings.Saturation} \n" +
				$"	Tint = {settings.Tint} \n" +
#endif
				$"	TransformCenterPoint = {settings.TransformCenterPoint} \n" +
				$"	Easing = {settings.Easing} \n" +
				$"	EasingMode = {settings.EasingMode} \n" +
				" ------------------------------------";

			Animations.Logger.Debug(output);
		}
	}
}