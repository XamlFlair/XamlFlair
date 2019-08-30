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
		// ====================
		// FADE
		// ====================

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
					ElementCompositionPreview.GetElementVisual(element).Opacity = (float)settings.Opacity;

					return animGroup.CreateScalarAnimation<FadeAnimation>(
						element,
						settings,
						to: 1);
				});
		}

		// ====================
		// TRANSLATE
		// ====================

		internal static void TranslateXTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<TranslateXAnimation>(
						element,
						settings,
						to: (float)element.GetCalculatedOffsetX(settings.OffsetX)));
		}

		internal static void TranslateYTo(this FrameworkElement element, AnimationSettings settings, ref AnimationGroup group)
		{
			group.CreateAnimations(element, settings,
				animGroup =>
					animGroup.CreateScalarAnimation<TranslateYAnimation>(
						element,
						settings,
						to: (float)element.GetCalculatedOffsetY(settings.OffsetY)));
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
					var visual = ElementCompositionPreview.GetElementVisual(element);

					// Since Translation doesn't exist as a property on Visual, we try to fetch it from the PropertySet
					if (visual.Properties.TryGetVector3(TargetProperties.Translation, out var translation) == CompositionGetValueStatus.Succeeded)
					{
						visual.Properties.InsertVector3(TargetProperties.Translation, new Vector3((float)element.GetCalculatedOffsetX(settings.OffsetX), translation.Y, translation.Z));
					}
					else
					{
						visual.Properties.InsertVector3(TargetProperties.Translation, new Vector3((float)element.GetCalculatedOffsetX(settings.OffsetX), 0f, 0f));
					}

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
					var visual = ElementCompositionPreview.GetElementVisual(element);

					// Since Translation doesn't exist as a property on Visual, we try to fetch it from the PropertySet
					if (visual.Properties.TryGetVector3(TargetProperties.Translation, out var translation) == CompositionGetValueStatus.Succeeded)
					{
						visual.Properties.InsertVector3(TargetProperties.Translation, new Vector3(translation.X, (float)element.GetCalculatedOffsetY(settings.OffsetY), translation.Z));
					}
					else
					{
						visual.Properties.InsertVector3(TargetProperties.Translation, new Vector3(0f, (float)element.GetCalculatedOffsetY(settings.OffsetY), 0f));
					}

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
					var visual = ElementCompositionPreview.GetElementVisual(element);

					// Since Translation doesn't exist as a property on Visual, we try to fetch it from the PropertySet
					if (visual.Properties.TryGetVector3(TargetProperties.Translation, out var translation) == CompositionGetValueStatus.Succeeded)
					{
						visual.Properties.InsertVector3(TargetProperties.Translation, new Vector3(translation.X, translation.Y, (float)settings.OffsetZ));
					}
					else
					{
						visual.Properties.InsertVector3(TargetProperties.Translation, new Vector3(0f, 0f, (float)settings.OffsetZ));
					}

					return animGroup.CreateScalarAnimation<TranslateZAnimation>(
						element,
						settings,
						to: 0f);
				});
		}

		// ====================
		// SCALE
		// ====================

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
					var visual = ElementCompositionPreview.GetElementVisual(element);

					visual.CenterPoint = element.GetTransformCenter(settings);
					visual.Scale = new Vector3((float)settings.ScaleX, visual.Scale.Y, visual.Scale.Z);

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
					var visual = ElementCompositionPreview.GetElementVisual(element);

					visual.CenterPoint = element.GetTransformCenter(settings);
					visual.Scale = new Vector3(visual.Scale.X, (float)settings.ScaleY, visual.Scale.Z);

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
					var visual = ElementCompositionPreview.GetElementVisual(element);

					visual.CenterPoint = element.GetTransformCenter(settings);
					visual.Scale = new Vector3(visual.Scale.X, visual.Scale.Y, (float)settings.ScaleZ);

					return animGroup.CreateScalarAnimation<ScaleZAnimation>(
						element,
						settings,
						to: 1f);
				});
		}

		// ====================
		// ROTATE
		// ====================

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
					var visual = ElementCompositionPreview.GetElementVisual(element);

					visual.CenterPoint = element.GetTransformCenter(settings);
					visual.RotationAngleInDegrees = (float)settings.Rotation;

					return animGroup.CreateScalarAnimation<RotateAnimation>(
						element,
						settings,
						to: 0);
				});
		}

		// ====================
		// BLUR
		// ====================

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

		// ====================
		// SATURATE
		// ====================

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

		// ====================
		// TINT
		// ====================

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

			visual.CenterPoint = element.GetTransformCenter(settings);

			if (settings.Opacity != 1)
			{
				visual.Opacity = (float)settings.Opacity;
			}

			if (settings.OffsetX != AnimationSettings.DEFAULT_TRANSLATION || settings.OffsetY != AnimationSettings.DEFAULT_TRANSLATION || settings.OffsetZ != 0)
			{
				visual.Properties.InsertVector3(
					TargetProperties.Translation,
					new Vector3(
						(float)element.GetCalculatedOffsetX(settings.OffsetX),
						(float)element.GetCalculatedOffsetY(settings.OffsetY),
						(float)settings.OffsetZ));
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

		internal static Vector3 GetTransformCenter(this FrameworkElement element, AnimationSettings settings)
		{
			// Based on the state of the element, try to get the width in the following precedence:
			//		ActualWidth > Width > MinWidth
			var elementWidth = element.ActualWidth > 0
				? element.ActualWidth
				: element.Width > 0
					? element.Width
					: element.MinWidth;

			// Based on the state of the element, try to get the height in the following precedence:
			//		ActualHeight > Height > MinHeight
			var elementHeight = element.ActualHeight > 0
				? element.ActualHeight
				: element.Height > 0
					? element.Height
					: element.MinHeight;

			var centerX = (float)(elementWidth * settings.TransformCenterPoint.X);
			var centerY = (float)(elementHeight * settings.TransformCenterPoint.Y);

			return new Vector3(centerX, centerY, 0f);
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