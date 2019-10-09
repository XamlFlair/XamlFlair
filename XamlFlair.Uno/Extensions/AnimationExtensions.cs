using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using XamlFlair.Extensions;
using XamlFlair.Uno.Logging;

namespace XamlFlair.Uno.Extensions
{
	internal static class AnimationExtensions
	{
		// ====================
		// FADE
		// ====================

		internal static Storyboard FadeTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			element.ApplyAnimation(settings, element.Opacity, settings.Opacity, "(UIElement.Opacity)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard FadeFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			element.Opacity = settings.Opacity;

			element.ApplyAnimation(settings, settings.Opacity, 1, "(UIElement.Opacity)", ref storyboard);

			return storyboard;
		}

		// ====================
		// TRANSLATE
		// ====================

		internal static Storyboard TranslateXTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, transform.TranslateX, settings.OffsetX, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateYTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, transform.TranslateY, settings.OffsetY, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateXFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();
			transform.TranslateX = settings.OffsetX;

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, settings.OffsetX, 0, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateYFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();
			transform.TranslateY = settings.OffsetY;

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, settings.OffsetY, 0, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)", ref storyboard);

			return storyboard;
		}

		// ====================
		// SCALE
		// ====================

		internal static Storyboard ScaleXTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, transform.ScaleX, settings.ScaleX, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard ScaleYTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, transform.ScaleY, settings.ScaleY, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard ScaleXFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();
			transform.ScaleX = settings.ScaleX;

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, settings.ScaleX, 1, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard ScaleYFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();
			transform.ScaleY = settings.ScaleY;

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, settings.ScaleY, 1, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)", ref storyboard);

			return storyboard;
		}

		// ====================
		// ROTATE
		// ====================

		internal static Storyboard RotateTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, transform.Rotation, settings.Rotation, "(UIElement.RenderTransform).(CompositeTransform.Rotation)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard RotateFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();
			transform.Rotation = settings.Rotation;

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, settings.Rotation, 0, "(UIElement.RenderTransform).(CompositeTransform.Rotation)", ref storyboard);

			return storyboard;
		}

		private static void SetRenderTransform(FrameworkElement element, AnimationSettings settings, CompositeTransform transform, bool updateTransformCenterPoint = false)
		{
			element.RenderTransform = transform;

			if (updateTransformCenterPoint)
			{
//#if __ANDROID__
//				transform.CenterX = element.ActualWidth * settings.RenderTransformOrigin.X;
//				transform.CenterY = element.ActualHeight * settings.RenderTransformOrigin.Y;
//#else
				element.RenderTransformOrigin = settings.TransformCenterPoint;
//#endif
			}
		}

		private static void ApplyAnimation(this FrameworkElement element, AnimationSettings settings, double from, double to, string targetProperty, ref Storyboard storyboard)
		{
			var anim = new DoubleAnimation()
			{
				From = from,
				To = to,
				Duration = new Duration(TimeSpan.FromMilliseconds(settings.Duration)),
				BeginTime = TimeSpan.FromMilliseconds(settings.Delay),
				EasingFunction = settings.GetEase()
			};

			Storyboard.SetTarget(anim, element);
			Storyboard.SetTargetProperty(anim, targetProperty);

			storyboard.Children.Add(anim);

			// If the element is ListBoxItem-based, we must check the logging property on its parent ListViewBase
			if (element is SelectorItem
				&& element.FindAscendant<ListViewBase>() is ListViewBase lvb
				&& Animations.GetEnableLogging(lvb))
			{
				// Log for a SelectorItem
				element.LogAnimationInfo(targetProperty, anim, settings);
			}
			else if (Animations.GetEnableLogging(element))
			{
				// Log for a FrameworkElement
				element.LogAnimationInfo(targetProperty, anim, settings);
			}
		}

		internal static void ApplyInitialSettings(this FrameworkElement element, AnimationSettings settings)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();

			transform.TranslateX = settings.OffsetX;
			transform.TranslateY = settings.OffsetY;
			transform.Rotation = settings.Rotation;
			transform.ScaleX = settings.ScaleX;
			transform.ScaleY = settings.ScaleY;

			element.Opacity = settings.Opacity;

			//#if __ANDROID__
			//			composite.CenterX = element.ActualWidth * settings.RenderTransformOrigin.X;
			//			composite.CenterY = element.ActualHeight * settings.RenderTransformOrigin.Y;
			//#else
			element.RenderTransformOrigin = settings.TransformCenterPoint;
			//#endif
			element.RenderTransform = transform;
		}

		private static void LogAnimationInfo(this FrameworkElement element, string targetProperty, DoubleAnimation anim, AnimationSettings settings)
		{
			// Build the "element" output with Name + Type if Name exists, else just the Type
			var name = element.Name ?? string.Empty;
			var elementOutput = !string.IsNullOrEmpty(name)
				? $"{name} ({element.GetType()})"
				: element.GetType().ToString();

			var output =
				"\n---------- ANIMATION ----------\n" +
				$"Timestamp = {DateTimeOffset.Now.ToString("HH:mm:ss:fffff")} \n" +
				" Animation: \n" +
					$"	Element = {elementOutput} \n" +
					$"	TargetProperty = {targetProperty} \n" +
					$"	From = {anim.From} \n" +
					$"	To = {anim.To} \n" +
				" Settings: \n" +
					$"	Kind = {settings.Kind} \n" +
					$"	Duration = {settings.Duration} \n" +
					$"	Delay = {settings.Delay} \n" +
					$"	Opacity = {settings.Opacity} \n" +
					$"	OffsetX = {settings.OffsetX} \n" +
					$"	OffsetY = {settings.OffsetY} \n" +
					$"	ScaleX = {settings.ScaleX} \n" +
					$"	ScaleY = {settings.ScaleY} \n" +
					$"	Rotation = {settings.Rotation} \n" +
					$"	TransformCenterPoint = {settings.TransformCenterPoint} \n" +
					$"	Easing = {settings.Easing} \n" +
					$"	EasingMode = {settings.EasingMode} \n" +
				"------------------------------------";

			Animations.Logger.Debug(output);
		}
	}
}