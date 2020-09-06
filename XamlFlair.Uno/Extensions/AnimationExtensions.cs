using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Extensions.Logging;
using XamlFlair.Extensions;
using Windows.UI;
using Windows.UI.Xaml.Shapes;

namespace XamlFlair.Extensions
{
	internal static class AnimationExtensions
	{
		internal static ILogger Logger;

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

			element.ApplyAnimation(settings, transform.TranslateX, settings.OffsetX.GetCalculatedOffset(element, OffsetTarget.X), "(UIElement.RenderTransform).(CompositeTransform.TranslateX)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateYTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, transform.TranslateY, settings.OffsetY.GetCalculatedOffset(element, OffsetTarget.Y), "(UIElement.RenderTransform).(CompositeTransform.TranslateY)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateXFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();
			transform.TranslateX = settings.OffsetX.GetCalculatedOffset(element, OffsetTarget.X);

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, transform.TranslateX, 0, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateYFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();
			transform.TranslateY = settings.OffsetY.GetCalculatedOffset(element, OffsetTarget.Y);

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, transform.TranslateY, 0, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)", ref storyboard);

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

		// ====================
		// COLOR
		// ====================

		internal static Storyboard ColorTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			Color fromColor = Colors.Transparent;
			var propertyPath = string.Empty;

			switch (settings.ColorOn)
			{
				case ColorTarget.Background when element is Control ctl && ctl.Background is SolidColorBrush brush:
					propertyPath = "(Control.Background).(SolidColorBrush.Color)";
					fromColor = brush.Color;
					break;

				case ColorTarget.Foreground when element is Control ctl && ctl.Foreground is SolidColorBrush brush:
					propertyPath = "(Control.Foreground).(SolidColorBrush.Color)";
					fromColor = brush.Color;
					break;

				case ColorTarget.BorderBrush when element is Control ctl && ctl.BorderBrush is SolidColorBrush brush:
					propertyPath = "(Control.BorderBrush).(SolidColorBrush.Color)";
					fromColor = brush.Color;
					break;

				case ColorTarget.Foreground when element is TextBlock tb && tb.Foreground is SolidColorBrush brush:
					propertyPath = "(TextBlock.Foreground).(SolidColorBrush.Color)";
					fromColor = brush.Color;
					break;

				case ColorTarget.Fill when element is Shape shp && shp.Fill is SolidColorBrush brush:
					propertyPath = "(Shape.Fill).(SolidColorBrush.Color)";
					fromColor = brush.Color;
					break;

				case ColorTarget.Stroke when element is Shape shp && shp.Stroke is SolidColorBrush brush:
					propertyPath = "(Shape.Stroke).(SolidColorBrush.Color)";
					fromColor = brush.Color;
					break;

				default:
					const string message =
						"$Cannot animate the ColorAnimation. Make sure the animation is applied on a Control, TextBlock, or Shape. " +
						"Also make sure that an existing brush exists on the corresponding property (Background, Foreground, BorderBrush, Fill, or Stroke).";
					throw new ArgumentException(message);
			}
			element.ApplyAnimation(settings, fromColor, settings.Color, propertyPath, ref storyboard);

			return storyboard;
		}

		internal static Storyboard ColorFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			Color toColor = Colors.Transparent;
			var propertyPath = string.Empty;

			switch (settings.ColorOn)
			{
				case ColorTarget.Background when element is Control ctl:
					propertyPath = "(Control.Background).(SolidColorBrush.Color)";
					toColor = (ctl.Background as SolidColorBrush)?.Color ?? Colors.Transparent;
					ctl.Background = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.Foreground when element is Control ctl:
					propertyPath = "(Control.Foreground).(SolidColorBrush.Color)";
					toColor = (ctl.Foreground as SolidColorBrush)?.Color ?? Colors.Transparent;
					ctl.Foreground = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.BorderBrush when element is Control ctl:
					propertyPath = "(Control.BorderBrush).(SolidColorBrush.Color)";
					toColor = (ctl.BorderBrush as SolidColorBrush)?.Color ?? Colors.Transparent;
					ctl.BorderBrush = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.Foreground when element is TextBlock tb:
					propertyPath = "(TextBlock.Foreground).(SolidColorBrush.Color)";
					toColor = (tb.Foreground as SolidColorBrush)?.Color ?? Colors.Transparent;
					tb.Foreground = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.Fill when element is Shape shp:
					propertyPath = "(Shape.Fill).(SolidColorBrush.Color)";
					toColor = (shp.Fill as SolidColorBrush)?.Color ?? Colors.Transparent;
					shp.Fill = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.Stroke when element is Shape shp:
					propertyPath = "(Shape.Stroke).(SolidColorBrush.Color)";
					toColor = (shp.Stroke as SolidColorBrush)?.Color ?? Colors.Transparent;
					shp.Stroke = new SolidColorBrush(settings.Color);
					break;

				default:
					// TODO: What do we do ???
					break;
			}

			element.ApplyAnimation(settings, settings.Color, toColor, propertyPath, ref storyboard);

			return storyboard;
		}

		private static void SetRenderTransform(FrameworkElement element, AnimationSettings settings, CompositeTransform transform, bool updateTransformCenterPoint = false)
		{
			element.RenderTransform = transform;

			if (updateTransformCenterPoint)
			{
				element.RenderTransformOrigin = settings.TransformCenterPoint;
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

			element.ApplyAnimationCore(anim, settings, from.ToString(), to.ToString(), targetProperty, ref storyboard);
		}

		private static void ApplyAnimation(this FrameworkElement element, AnimationSettings settings, Color from, Color to, string targetProperty, ref Storyboard storyboard)
		{
			var anim = new ColorAnimation()
			{
				From = from,
				To = to,
				Duration = new Duration(TimeSpan.FromMilliseconds(settings.Duration)),
				BeginTime = TimeSpan.FromMilliseconds(settings.Delay),
				EasingFunction = settings.GetEase()
			};

			element.ApplyAnimationCore(anim, settings, from.ToString(), to.ToString(), targetProperty, ref storyboard);
		}

		private static void ApplyAnimationCore(this FrameworkElement element, Timeline animation, AnimationSettings settings, string from, string to, string targetProperty, ref Storyboard storyboard)
		{
			Storyboard.SetTarget(animation, element);
			Storyboard.SetTargetProperty(animation, targetProperty);

			storyboard.Children.Add(animation);

			// If the element is ListBoxItem-based, we must check the logging property on its parent ListViewBase
			if (element is SelectorItem
				&& element.FindAscendant<ListViewBase>() is ListViewBase lvb
				&& Animations.GetEnableLogging(lvb) == LogLevel.Debug)
			{
				// Log for a SelectorItem
				element.LogAnimationInfo(targetProperty, from, to, settings);
			}
			else if (Animations.GetEnableLogging(element) == LogLevel.Debug)
			{
				// Log for a FrameworkElement
				element.LogAnimationInfo(targetProperty, from, to, settings);
			}
		}

		internal static void ApplyInitialSettings(this FrameworkElement element, AnimationSettings settings)
		{
			var transform = (element.RenderTransform as CompositeTransform) ?? new CompositeTransform();

			transform.TranslateX = settings.OffsetX.GetCalculatedOffset(element, OffsetTarget.X);
			transform.TranslateY = settings.OffsetY.GetCalculatedOffset(element, OffsetTarget.Y);
			transform.Rotation = settings.Rotation;
			transform.ScaleX = settings.ScaleX;
			transform.ScaleY = settings.ScaleY;

			element.Opacity = settings.Opacity;

			element.RenderTransformOrigin = settings.TransformCenterPoint;
			element.RenderTransform = transform;
		}

		private static void LogAnimationInfo(this FrameworkElement element, string targetProperty, string from, string to, AnimationSettings settings)
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
					$"	From = {from} \n" +
					$"	To = {to} \n" +
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

			Logger?.LogDebug(output);
		}
	}
}