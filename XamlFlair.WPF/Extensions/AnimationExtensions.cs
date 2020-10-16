using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace XamlFlair.Extensions
{
	internal static class AnimationExtensions
	{
		internal static ILogger Logger;

		private const short SCALE_INDEX = 0;
		private const short ROTATE_INDEX = 2;
		private const short TRANSLATE_INDEX = 3;

		// ====================
		// FADE
		// ====================

		internal static Storyboard FadeTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			// Since a previous animation can have a "hold" on the Opacity property, we must "release" it before
			// setting a new value. See the Remarks section of the AttachedProperty for more info.
			if (Animations.GetAllowOpacityReset(element))
			{
				element.BeginAnimation(UIElement.OpacityProperty, null);
			}

			element.ApplyAnimation(settings, element.Opacity, settings.Opacity, "Opacity", ref storyboard);

			return storyboard;
		}

		internal static Storyboard FadeFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			// Since a previous animation can have a "hold" on the Opacity property, we must "release" it before
			// setting a new value. See the Remarks section of the AttachedProperty for more info.
			if (Animations.GetAllowOpacityReset(element))
			{
				element.BeginAnimation(UIElement.OpacityProperty, null);
			}

			element.Opacity = settings.Opacity;

			element.ApplyAnimation(settings, settings.Opacity, 1, "Opacity", ref storyboard);

			return storyboard;
		}

		// ====================
		// TRANSLATE
		// ====================

		internal static Storyboard TranslateXTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var translate = transform.Children[TRANSLATE_INDEX] as TranslateTransform;

			SetTransform(element, settings, transform);

			element.ApplyAnimation(settings, translate.X, settings.OffsetX.GetCalculatedOffset(element, OffsetTarget.X),
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{TRANSLATE_INDEX}].X", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateYTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var translate = transform.Children[TRANSLATE_INDEX] as TranslateTransform;

			SetTransform(element, settings, transform);

			element.ApplyAnimation(settings, translate.Y, settings.OffsetY.GetCalculatedOffset(element, OffsetTarget.Y),
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{TRANSLATE_INDEX}].Y", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateXFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var translate = transform.Children[TRANSLATE_INDEX] as TranslateTransform;

			translate.X = settings.OffsetX.GetCalculatedOffset(element, OffsetTarget.X);

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, translate.X, 0,
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{TRANSLATE_INDEX}].X", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateYFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var translate = transform.Children[TRANSLATE_INDEX] as TranslateTransform;

			translate.Y = settings.OffsetY.GetCalculatedOffset(element, OffsetTarget.Y);

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, translate.Y, 0,
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{TRANSLATE_INDEX}].Y", ref storyboard);

			return storyboard;
		}

		// ====================
		// SCALE
		// ====================

		internal static Storyboard ScaleXTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = GetFrameworkElementTransform(element, settings) ?? CreateTransformGroup();
			var scale = transform.Children[SCALE_INDEX] as ScaleTransform;

			SetTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, scale.ScaleX, settings.ScaleX,
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{SCALE_INDEX}].ScaleX", ref storyboard);

			return storyboard;
		}

		internal static Storyboard ScaleYTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = GetFrameworkElementTransform(element, settings) ?? CreateTransformGroup();
			var scale = transform.Children[SCALE_INDEX] as ScaleTransform;

			SetTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, scale.ScaleY, settings.ScaleY,
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{SCALE_INDEX}].ScaleY", ref storyboard);

			return storyboard;
		}

		internal static Storyboard ScaleXFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = GetFrameworkElementTransform(element, settings) ?? CreateTransformGroup();
			var scale = transform.Children[SCALE_INDEX] as ScaleTransform;

			scale.ScaleX = settings.ScaleX;

			SetTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, settings.ScaleX, 1,
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{SCALE_INDEX}].ScaleX", ref storyboard);

			return storyboard;
		}

		internal static Storyboard ScaleYFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = GetFrameworkElementTransform(element, settings) ?? CreateTransformGroup();
			var scale = transform.Children[SCALE_INDEX] as ScaleTransform;

			scale.ScaleY = settings.ScaleY;

			SetTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, settings.ScaleY, 1,
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{SCALE_INDEX}].ScaleY", ref storyboard);

			return storyboard;
		}

		// ====================
		// ROTATE
		// ====================

		internal static Storyboard RotateTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = GetFrameworkElementTransform(element, settings) ?? CreateTransformGroup();
			var rotate = transform.Children[ROTATE_INDEX] as RotateTransform;

			SetTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, rotate.Angle, settings.Rotation,
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{ROTATE_INDEX}].Angle", ref storyboard);

			return storyboard;
		}

		internal static Storyboard RotateFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = GetFrameworkElementTransform(element, settings) ?? CreateTransformGroup();
			var rotate = transform.Children[ROTATE_INDEX] as RotateTransform;

			rotate.Angle = settings.Rotation;

			SetTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, settings.Rotation, 0,
				$"{(settings.TransformOn == TransformationType.Render ? "Render" : "Layout")}Transform.Children[{ROTATE_INDEX}].Angle", ref storyboard);

			return storyboard;
		}

		// ====================
		// COLOR
		// ====================

		internal static Storyboard ColorTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var fromColor = Colors.Transparent;
			var propertyPath = string.Empty;
			var elementType = element.GetType().Name;
			const string brushTarget = "SolidColorBrush.Color";

			switch (settings.ColorOn)
			{
				case ColorTarget.Background when element is Control ctl && ctl.Background is SolidColorBrush brush:
					propertyPath = $"({elementType}.Background).({brushTarget})";
					fromColor = brush.Color;
					break;

				case ColorTarget.Background when element is Panel pnl && pnl.Background is SolidColorBrush brush:
					propertyPath = $"({elementType}.Background).({brushTarget})";
					fromColor = brush.Color;
					break;

				case ColorTarget.Foreground when element is Control ctl && ctl.Foreground is SolidColorBrush brush:
					propertyPath = $"({elementType}.Foreground).({brushTarget})";
					fromColor = brush.Color;
					break;

				case ColorTarget.BorderBrush when element is Control ctl && ctl.BorderBrush is SolidColorBrush brush:
					propertyPath = $"({elementType}.BorderBrush).({brushTarget})";
					fromColor = brush.Color;
					break;

				case ColorTarget.Foreground when element is TextBlock tb && tb.Foreground is SolidColorBrush brush:
					propertyPath = $"({elementType}.Foreground).({brushTarget})";
					fromColor = brush.Color;
					break;

				case ColorTarget.Fill when element is Shape shp && shp.Fill is SolidColorBrush brush:
					propertyPath = $"({elementType}.Fill).({brushTarget})";
					fromColor = brush.Color;
					break;

				case ColorTarget.Stroke when element is Shape shp && shp.Stroke is SolidColorBrush brush:
					propertyPath = $"({elementType}.Stroke).({brushTarget})";
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
			var toColor = Colors.Transparent;
			var propertyPath = string.Empty;
			var elementType = element.GetType().Name;
			const string brushTarget = "SolidColorBrush.Color";

			switch (settings.ColorOn)
			{
				case ColorTarget.Background when element is Control ctl:
					propertyPath = $"(Control.Background).({brushTarget})";
					toColor = (ctl.Background as SolidColorBrush)?.Color ?? Colors.Transparent;
					ctl.Background = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.Foreground when element is Control ctl:
					propertyPath = $"(Control.Foreground).({brushTarget})";
					toColor = (ctl.Foreground as SolidColorBrush)?.Color ?? Colors.Transparent;
					ctl.Foreground = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.BorderBrush when element is Control ctl:
					propertyPath = $"(Control.BorderBrush).({brushTarget})";
					toColor = (ctl.BorderBrush as SolidColorBrush)?.Color ?? Colors.Transparent;
					ctl.BorderBrush = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.Foreground when element is TextBlock tb:
					propertyPath = $"(TextBlock.Foreground).({brushTarget})";
					toColor = (tb.Foreground as SolidColorBrush)?.Color ?? Colors.Transparent;
					tb.Foreground = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.Fill when element is Shape shp:
					propertyPath = $"(Shape.Fill).({brushTarget})";
					toColor = (shp.Fill as SolidColorBrush)?.Color ?? Colors.Transparent;
					shp.Fill = new SolidColorBrush(settings.Color);
					break;

				case ColorTarget.Stroke when element is Shape shp:
					propertyPath = $"(Shape.Stroke).({brushTarget})";
					toColor = (shp.Stroke as SolidColorBrush)?.Color ?? Colors.Transparent;
					shp.Stroke = new SolidColorBrush(settings.Color);
					break;

				default:
					const string message =
						"$Cannot animate the ColorAnimation. Make sure the animation is applied on a Control, TextBlock, or Shape. " +
						"Also make sure that an existing brush exists on the corresponding property (Background, Foreground, BorderBrush, Fill, or Stroke).";
					throw new ArgumentException(message);
			}

			element.ApplyAnimation(settings, settings.Color, toColor, propertyPath, ref storyboard);

			return storyboard;
		}

		// ====================
		// BLUR
		// ====================

		internal static Storyboard BlurTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var effect = (element.Effect as BlurEffect) ?? new BlurEffect();
			element.Effect = effect;

			element.ApplyAnimation(settings, effect.Radius, settings.BlurRadius, "Effect.Radius", ref storyboard);

			return storyboard;
		}

		internal static Storyboard BlurFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			element.Effect = (element.Effect as BlurEffect) ?? new BlurEffect();

			element.ApplyAnimation(settings, settings.BlurRadius, 0, "Effect.Radius", ref storyboard);

			return storyboard;
		}

		private static TransformGroup GetFrameworkElementTransform(FrameworkElement element, AnimationSettings settings)
		{
			return settings.TransformOn == TransformationType.Layout ?
				(element.LayoutTransform as TransformGroup) :
				(element.RenderTransform as TransformGroup);
		}

		private static void SetTransform(FrameworkElement element, AnimationSettings settings, TransformGroup transform, bool updateTransformCenterPoint = false)
		{
			if (settings.TransformOn == TransformationType.Layout)
			{
				SetLayoutTransform(element, settings, transform, updateTransformCenterPoint: true);
			}
			else
			{
				SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);
			}
		}

		private static void SetRenderTransform(FrameworkElement element, AnimationSettings settings, TransformGroup transform, bool updateTransformCenterPoint = false)
		{
			element.RenderTransform = transform;

			if (updateTransformCenterPoint)
			{
				element.RenderTransformOrigin = settings.TransformCenterPoint;
			}
		}

		private static void SetLayoutTransform(FrameworkElement element, AnimationSettings settings, TransformGroup transform, bool updateTransformCenterPoint = false)
		{
			element.LayoutTransform = transform;
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
			Storyboard.SetTargetProperty(animation, new PropertyPath(targetProperty));

			storyboard.Children.Add(animation);

			// If the element is ListBoxItem-based, we must check the logging property on its parent ListBox
			if (element is ListBoxItem
				&& element.FindAscendant<ListBox>() is ListBox lb
				&& Animations.GetEnableLogging(lb) == LogLevel.Debug)
			{
				// Log for a ListBoxItem
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
			var transform = GetFrameworkElementTransform(element, settings) ?? CreateTransformGroup();
			var rotate = (transform.Children[ROTATE_INDEX] as RotateTransform);
			var scale = (transform.Children[SCALE_INDEX] as ScaleTransform);
			var translate = (transform.Children[TRANSLATE_INDEX] as TranslateTransform);

			rotate.Angle = settings.Rotation;
			scale.ScaleX = settings.ScaleX;
			scale.ScaleY = settings.ScaleY;
			translate.X = settings.OffsetX.GetCalculatedOffset(element, OffsetTarget.X);
			translate.Y = settings.OffsetY.GetCalculatedOffset(element, OffsetTarget.Y);

			// Since a previous animation can have a "hold" on the Opacity property, we must "release" it before
			// setting a new value. See the Remarks section of the AttachedProperty for more info.
			if (Animations.GetAllowOpacityReset(element))
			{
				element.BeginAnimation(UIElement.OpacityProperty, null);
			}

			element.Opacity = settings.Opacity;

			element.RenderTransformOrigin = settings.TransformCenterPoint;
			element.RenderTransform = transform;

			var effect = (element.Effect as BlurEffect) ?? new BlurEffect()
			{
				Radius = settings.BlurRadius
			};
			element.Effect = effect;
		}

		private static TransformGroup CreateTransformGroup()
		{
			var group = new TransformGroup();

			group.Children.Add(new ScaleTransform(1, 1));
			group.Children.Add(new SkewTransform(0, 0));
			group.Children.Add(new RotateTransform(0));
			group.Children.Add(new TranslateTransform(0, 0));

			return group;
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
					$"	Blur = {settings.BlurRadius} \n" +
					$"	TransformCenterPoint = {settings.TransformCenterPoint} \n" +
					$"	Easing = {settings.Easing} \n" +
					$"	EasingMode = {settings.EasingMode} \n" +
				"------------------------------------";

			Logger?.LogDebug(output);
		}
	}
}